using Day1lab.Model;
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
    public class ShoppingCart : ControllerBase
    {
        private Productcontext Context;

        public ShoppingCart()
        {
            this.Context = new Productcontext();
        }


        //[HttpGet("{id:int}", Name = "getOneCartRoute")] //api/ShoppingCart/

        //public IActionResult getByID(int id)
        //{
        //    CartItem cartItem = Context.ShoppingCart.FirstOrDefault(cart => cart.ID == id);
        //    if (cartItem == null)
        //    {
        //        return BadRequest("Cart not found");
        //    }
        //    return Ok(cartItem);
        //}

        //[HttpPost]
        //public IActionResult New( CartItem cartItem)//create cart and add prod
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
                 
        //            Context.ShoppingCart.Add(cartItem);
        //            Context.SaveChanges();
        //            string url = Url.Link("getOneCartRoute",
        //                new { id = cartItem.ID });
        //            return Created(url, cartItem);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    return BadRequest(ModelState);

        //}

        //[HttpPost("{id:int,quantity:int}")]
        //public IActionResult AddToCart([FromRoute] CartItem[] cartItem)//create cart and add prod
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            //int id = cartItem.Product_ID;
        //            //Product product = Context.Product.FirstOrDefault(p => p.ID == id);
        //            //cartItem.Product_ID = product.ID;
        //            //cartItem.Product_Name = product.Name;
        //            //cartItem.ImgURL = product.ImgURL;
        //            //product.Price = product.Price;

                    
        //            Context.SaveChanges();
        //            string url = Url.Link("getOneCartRoute",
        //                new { id = cartItem.ID });
        //            return Created(url, cartItem);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    return BadRequest(ModelState);

        //}
        //[HttpPut("{id:int}")]
        //public IActionResult Edit(int id, CartItems cartItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            CartItems UpdatedCart = Context.ShoppingCart.FirstOrDefault(cart => cart.ID == id);
        //            UpdatedCart.Product_Name = cartItem.Product_Name;
        //            UpdatedCart.Product_Price = cartItem.Product_Price;
        //            UpdatedCart.Product_Quantity= cartItem.Product_Quantity;
        //            UpdatedCart.ImgURL = cartItem.ImgURL;
        //           Context.SaveChanges();


        //            return Ok("Data saved");
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    return BadRequest(ModelState);

        //}
        //[HttpDelete("{id:int}")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        CartItems cartItem = Context.ShoppingCart.FirstOrDefault(prod => prod.ID == id);
        //        if (cartItem != null)
        //        {
        //            Context.ShoppingCart.Remove(cartItem);
        //            productcontext.SaveChanges();

        //        }
        //        return Ok("Product Deleted");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

    }
}

