using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Day1lab.Model
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual List<Product> productlist { get; set; }
    }
}
