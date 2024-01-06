using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string CoffeeName { get; set; }
        public string? AddinName { get; set; }

        public int? CoffeePrice { get; set; }

        public int? AddinPrice { get; set; }

        public int Qty { get; set; }
        public int TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }


}
