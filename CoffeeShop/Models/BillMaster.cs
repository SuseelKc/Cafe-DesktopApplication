using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{

    public class BillMaster
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CustomerName { get; set; }

        public string Contact { get; set; }

        public double TotalPrice { get; set; }



    }

}

