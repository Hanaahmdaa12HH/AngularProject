using System;
using System.Collections.Generic;
using System.Linq;
using Day1lab.Model;
using System.Threading.Tasks;

namespace Day1lab.DTO
{
    public class CategoryDTO
    {
        public int Cat_ID { get; set; }
        public string Cat_Name { get; set; }

        public List<ProductDTO> products { get; set; } = new List<ProductDTO>();

        //public List<Product> products { get; set; } = new List<Product>(); 
        //public List<string> products { get; set; } = new List<string>();
    }
}
