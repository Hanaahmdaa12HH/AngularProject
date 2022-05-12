using Day1lab.DTO;
using Day1lab.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day1lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private Productcontext Context;

        public OrderController()
        {
            this.Context = new Productcontext();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Post(Order order)
        {
            List<CartItem> productsWithAvaliableQuantity = new List<CartItem>();
            CartItem cartItem;
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in order.Products)
                    {
                        Product product = Context.Product.FirstOrDefault(prod => prod.ID == item.ProductID);
                        if (item.Product_Quantity <= product.Quantity)
                        {
                            productsWithAvaliableQuantity.Add(item);
                            product.Quantity -= item.Product_Quantity;
                          
                        }
                        else
                            return BadRequest("Q=This quantity of {item.Product_Name} not Exist");

                    }
                    order.Products = productsWithAvaliableQuantity;
                    Context.Order.Add(order);
                     foreach (var item in order.Products)
                    {
                        cartItem = new CartItem();
                        cartItem.OrderID = order.ID;
                        cartItem.ProductID = item.ProductID;
                        cartItem.Product_Quantity = item.Product_Quantity;
                        Context.CartItems.Add(cartItem);
                        
                    }
                    Context.SaveChanges();
                    //string url = Url.Link("getOneRouteCategory", new { id = category.ID });
                    //return Created(url, category);
                    return Ok("Order Created");
                }

                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

    }
}
