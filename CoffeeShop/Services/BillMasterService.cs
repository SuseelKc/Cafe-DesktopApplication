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
        
        //find through contact where how many order is made by the specific customer 
        public static int CountByContact(string contactNumber)
        {
            var billMasters = GetAll();
            return billMasters.Count(bill => bill.Contact == contactNumber);
        }

        public static int CountConsecutiveByContact(string contactNumber, DateTime startDate, DateTime endDate)
        {
            var billMasters = GetAll();

            int consecutiveCount = 0;
            int currentConsecutiveCount = 0;
            DateTime currentDate = startDate.Date;

            while (currentDate <= endDate.Date)
            {
                bool isDatePresent = billMasters.Any(bill => bill.Contact == contactNumber && bill.CreatedAt.Date == currentDate);

                if (isDatePresent)
                {
                    currentConsecutiveCount++;
                }
                else
                {
                    consecutiveCount = Math.Max(consecutiveCount, currentConsecutiveCount);
                    currentConsecutiveCount = 0;
                }

                currentDate = currentDate.AddDays(1);
            }

            return consecutiveCount;
        }


        //adding the vlaue
        public static List<BillMaster> AddBillMaster(int Id, DateTime CreatedAt, string Customername, string Contact, double TotalPrice)
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
