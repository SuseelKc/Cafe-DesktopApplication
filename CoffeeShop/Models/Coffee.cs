using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Coffee
    {
        public int Id { get; set; }
        public string CoffeeName { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
    }
}
