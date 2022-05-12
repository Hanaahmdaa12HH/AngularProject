using Day1lab.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Day1lab.Model
{
    public class CartItem
    {
        public int ID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [ForeignKey("order")]
        public int OrderID { get; set; }
        //public string Product_Name { get; set; }
        //public double Product_Price { get; set; }
        //public string ImgURL { get; set; } 
        public int Product_Quantity { get; set; }



        [JsonIgnore]
        public virtual Product Product { get; set; }
        public virtual Order order { get; set; }
    }
}

