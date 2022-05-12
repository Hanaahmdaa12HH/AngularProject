using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Day1lab.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int  Price { get; set; }
        public int Quantity { get; set; }
        public string ImgURL { get; set; }

        [ForeignKey("Category") ]
        public int CategoryID { get; set; }

        [JsonIgnore]
        public virtual Category category { get; set; }
    }
}
