using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Models;
using System.Text.Json;

namespace CoffeeShop.Services
{
    public class BillDetailService
    {
        private static void SaveAll(List<BillDetail> billdetail)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetBillDetailFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(billdetail);
            File.WriteAllText(appUsersFilePath, json);
        }

        //getting back the saved value
        public static List<BillDetail> GetAll()
        {
            string appUsersFilePath = Utils.GetBillDetailFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<BillDetail>();
            }

            var json = File.ReadAllText(appUsersFilePath);
            return JsonSerializer.Deserialize<List<BillDetail>>(json);
        }

        //adding the vlaue
        public static List<BillDetail> AddBillDetail(int Id, int BillMasterId, string CoffeeName, string AddinName, int CoffeePrice, int AddinPrice, int TotalPrice)
        {
            List<BillDetail> billDetail = GetAll(); // Retrieve the existing list

            // Create a new coffee object with the provided values
            BillDetail newbillDetail = new BillDetail
            {
                Id = Id,
                BillMasterId = BillMasterId,
                CoffeeName = CoffeeName,
                AddinName = AddinName,
                CoffeePrice = CoffeePrice,
                AddinPrice = AddinPrice,
                TotalPrice = TotalPrice

            };

            billDetail.Add(newbillDetail); // Add the new coffee to the list

            SaveAll(billDetail); // Save the updated list to the JSON file

            return billDetail;

        }

        //public static List<BillDetail> DeleteOrder(int Id)
        //{

        //    List<BillDetail> Bill = GetAll();
        //    var orderToDelete = order.FirstOrDefault(c => c.Id == Id);

        //    if (orderToDelete != null)
        //    {
        //        order.Remove(orderToDelete);
        //        SaveAll(order);

        //    }

        //    return order;

        //}

        //public static void ClearAllOrders()

        //{

        //    List<BillDetail> orders = GetAll();


        //    if (orders != null)
        //    {
        //        orders.Clear(); // Clear method to removing all items from the collection
        //        SaveAll(orders);

        //    }



        //}

    }
}

