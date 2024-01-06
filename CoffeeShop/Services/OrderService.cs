using CoffeeShop.Components.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CoffeeShop.Models;
using Microsoft.Maui.Controls;

namespace CoffeeShop.Services
{
    public class OrderService
    {

        private static void SaveAll(List<Models.Order> addins)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetOrderFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(addins);
            File.WriteAllText(appUsersFilePath, json);
        }

        //getting back the saved value
        public static List<Models.Order> GetAll()
        {
            string appUsersFilePath = Utils.GetOrderFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<Models.Order>();
            }

            var json = File.ReadAllText(appUsersFilePath);
            return JsonSerializer.Deserialize<List<Models.Order>>(json);
        }

        //adding the vlaue
        public static List<Models.Order> AddOrder(int Id, string CustomerName,string CoffeeName, string AddinName,int CoffeePrice, int AddinPrice,int Qty,int TotalPrice)
        {
            List<Models.Order> order = GetAll(); // Retrieve the existing list

            // Create a new coffee object with the provided values
            Models.Order newOrder = new Models.Order
            {
                Id = Id,
                CustomerName = CustomerName,

                CoffeeName = CoffeeName,
                AddinName= AddinName,
                CoffeePrice= CoffeePrice,
                AddinPrice= AddinPrice,
                Qty= Qty,
                TotalPrice= TotalPrice

            };

            order.Add(newOrder); // Add the new coffee to the list

            SaveAll(order); // Save the updated list to the JSON file

            return order;

        }

        public static List<Models.Order> DeleteOrder(int Id)
        {

            List<Models.Order> order = GetAll();
            var orderToDelete = order.FirstOrDefault(c => c.Id == Id);

            if (orderToDelete != null)
            {
                order.Remove(orderToDelete);
                SaveAll(order);

            }

            return order;

        }

        public static void ClearAllOrders()

        {

            List<Models.Order> orders = GetAll();
           

            if (orders != null)
            {
                orders.Clear(); // Clear method to removing all items from the collection
                SaveAll(orders);

            }

        

        }

    }
}

