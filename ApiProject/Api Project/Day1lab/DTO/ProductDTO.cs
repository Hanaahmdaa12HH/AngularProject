using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day1lab.DTO
{
    public class ProductDTO //viewmodel
    {
          
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public double Product_Price { get; set; }
        public int Product_Quantity { get; set; }
        public string ImgURL { get; set; }
    }
}
