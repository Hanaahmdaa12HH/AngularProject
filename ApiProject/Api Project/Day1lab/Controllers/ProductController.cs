using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Day1lab.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Day1lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private Productcontext productcontext;

        public ProductController(Productcontext productcontext)
        {
            this.productcontext = productcontext;
        }
        
       
        [HttpGet]
        public IActionResult getAll()
        {
            List<Product>ProdtList = productcontext.Product.ToList();
            if (ProdtList == null)
            {
                return BadRequest("No Product to show");
            }
            return Ok(ProdtList);
        }

        [HttpGet("{id:int}", Name = "getOneRoute")]//api/deprtment/3

        public IActionResult getByID(int id)
        {
            Product product = productcontext.Product.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return BadRequest("Product not found");
            }
            return Ok(product);
        }
      


        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult New(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productcontext.Product.Add(product);
                    productcontext.SaveChanges();
                    string url = Url.Link("getOneRoute", new { id = product.ID });
                    return Created(url, product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product UpdatedProd = productcontext.Product.FirstOrDefault(p => p.ID == id);
                    UpdatedProd.Name = product.Name;
                    UpdatedProd.Price = product.Price;
                    UpdatedProd.Quantity = product.Quantity;
                    UpdatedProd.ImgURL = product.ImgURL;
                    UpdatedProd.CategoryID = product.CategoryID;
                    productcontext.SaveChanges();
                 

                    return Ok("Data saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct (int id)
        {
            try
            {
                Product prod = productcontext.Product.FirstOrDefault(prod => prod.ID == id);
                if(prod != null)
                {
                    productcontext.Product.Remove(prod);
                    productcontext.SaveChanges();
                   
                }
                return Ok("Product Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
    }

}

