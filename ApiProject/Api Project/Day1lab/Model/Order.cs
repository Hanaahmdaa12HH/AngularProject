using System;
using System.Collections.Generic;
using System.Linq;
using Day1lab.DTO;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Day1lab.Model
{
    public class Order
    {
       public int ID { get; set; }
       public string Address { get; set; }
        public string Date { get; set; } 
       public Double TotalPrice { get; set; }

        [ForeignKey("user")]
       public string UserID { get; set; }
    public List<CartItem> Products { get; set; } = new List<CartItem>();

      [JsonIgnore]
    public virtual ApplicationUser user { get; set; }
       
    }
}
