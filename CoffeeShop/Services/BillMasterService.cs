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



        public static bool CountAndCheckDiscount(string contactNumber)
        {
            var billMasters = GetAll();

            // Filter bills for the specific contact number
            var customerBills = billMasters.Where(bill => bill.Contact == contactNumber).OrderBy(bill => bill.CreatedAt).ToList();

            // Initialize variables for counting consecutive days
            int consecutiveCount = 0;
            DateTime previousDate = DateTime.MinValue;

            foreach (var bill in customerBills)
            {
                // Check if the current bill is on the next consecutive day
                if (bill.CreatedAt.Date == previousDate.AddDays(1).Date)
                {
                    consecutiveCount++;
                }
                else
                {
                    // Reset the count if there is a gap in consecutive dates
                    consecutiveCount = 1;
                }

                // Update the previous date for the next iteration
                previousDate = bill.CreatedAt.Date;

                // Check if the customer has continuous purchases for more than 30 days
                if (consecutiveCount > 30)
                {
                    return true; //  GetDiscount  to true after 30 days regular 
                }
            }

            return false; // Set GetDiscount flag to false
        }




    }
}
