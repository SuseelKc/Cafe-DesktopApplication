using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{

    public class BillDetail
    {
        public int Id { get; set; }
        
        public int BillMasterId { get; set; }

        public string CoffeeName { get; set; }
        public string? AddinName { get; set; }

        public int? CoffeePrice { get; set; }

        public int? AddinPrice { get; set; }

        public int TotalPrice { get; set; }


    }

}
