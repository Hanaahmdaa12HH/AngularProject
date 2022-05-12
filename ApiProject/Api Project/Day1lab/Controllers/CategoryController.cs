using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Day1lab.Model;
using System.Threading.Tasks;
using Day1lab.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Day1lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private Productcontext productcontext;

        public CategoryController(Productcontext productcontext)
        {
            this.productcontext = productcontext;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Category> CategoryList = productcontext.Category.ToList();
            if (CategoryList == null)
            {
                return BadRequest("No Product to show");
            }
            return Ok(CategoryList);
        }


        [HttpGet("Details/{CatID:int}")]
        public IActionResult getCategoryWithProd(int CatID)

        {
            ProductDTO productDTO ;
            Category category = productcontext.Category.Include(cat=>cat.productlist).
                FirstOrDefault(Cat => Cat.ID == CatID);

            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Cat_ID = category.ID,
                Cat_Name = category.Name
            };
            foreach (var item in category.productlist)
            {
                productDTO = new ProductDTO();
                productDTO.Product_Name = item.Name;
                productDTO.Product_ID = item.ID;
                productDTO.Product_Quantity = item.Quantity;
                productDTO.ImgURL = item.ImgURL;
                productDTO.Product_Price = item.Price;
               
                categoryDTO.products.Add(productDTO);
            }

            if (category == null)
            {
                return BadRequest("Product not found");
            }
            return Ok(categoryDTO);
        }

        [HttpGet("{id:int}", Name = "getOneRouteCategory")]//api/deprtment/3

        public IActionResult getByID(int id)
        {
            Category category= productcontext.Category.FirstOrDefault(cat => cat.ID == id);
            if (category == null)
            {
                return BadRequest("Product not found");
            }
            return Ok(category);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Post(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productcontext.Category.Add(category);
                    productcontext.SaveChanges();
                    string url = Url.Link("getOneRouteCategory", new { id = category.ID });
                    return Created(url, category);
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
        public IActionResult Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category UpdatedCat = productcontext.Category.FirstOrDefault(Cat => Cat.ID == id);
                    UpdatedCat.Name = category.Name;
                    productcontext.SaveChanges();


                    //return StatusCode(204, "Data Saved");
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
        public IActionResult Delete(int id)
        {
            try
            {
                Category category = productcontext.Category.FirstOrDefault(Cat => Cat.ID == id);
                if (category != null)
                {
                    productcontext.Remove(category);
                    productcontext.SaveChanges();

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}

