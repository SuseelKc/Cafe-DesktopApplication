using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Models;
using System.Text.Json;

namespace CoffeeShop.Services
{
    public class BillMasterService
    {
        private static void SaveAll(List<BillMaster> billMaster)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetBillMasterFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(billMaster);
            File.WriteAllText(appUsersFilePath, json);
        }

        //getting back the saved value
        public static List<BillMaster> GetAll()
        {
            string appUsersFilePath = Utils.GetBillMasterFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<BillMaster>();
            }

            var json = File.ReadAllText(appUsersFilePath);
            return JsonSerializer.Deserialize<List<BillMaster>>(json);
        }

        //adding the vlaue
        public static List<BillMaster> AddBillMaster(int Id, DateTime CreatedAt, string Customername, string Contact, int TotalPrice)
        {
            List<BillMaster> billMaster = GetAll(); // Retrieve the existing list

            // Create a new coffee object with the provided values
            BillMaster newbillMaster = new BillMaster
            {
                Id = Id,
                CreatedAt = CreatedAt,
                CustomerName = Customername,
                Contact = Contact,
                TotalPrice = TotalPrice

            };

            billMaster.Add(newbillMaster); // Add the new coffee to the list

            SaveAll(billMaster); // Save the updated list to the JSON file

            return billMaster;

        }


    }
}
