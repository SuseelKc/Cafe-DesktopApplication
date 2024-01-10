using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public class TopSellingService
    {

        public static List<TopSellingItem> GetTopSellingItems(List<BillDetail> billDetails, Func<BillDetail, string> selector)
        {
            var topSellingItems = billDetails
                .Where(b => !string.IsNullOrEmpty(selector(b)))
                .GroupBy(selector)
                .Select(g => new TopSellingItem
                {
                    Name = g.Key,
                    QuantitySold = g.Sum(x => x.Qty)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToList();

            return topSellingItems;
        }
    }
}
