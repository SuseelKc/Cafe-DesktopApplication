using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Components;

namespace CoffeeShop.Services
{
    public class CoffeeService
    {

        //saving the file to json
        private static void SaveAll(List<Coffee> coffee)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetCoffeeFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(coffee);
            File.WriteAllText(appUsersFilePath, json);
        }

        //getting back the saved value
        public static List<Coffee> GetAll()
        {
            string appUsersFilePath = Utils.GetCoffeeFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<Coffee>();
            }

            var json = File.ReadAllText(appUsersFilePath);
            return JsonSerializer.Deserialize<List<Coffee>>(json);
        }

        //adding the vlaue
        public static List<Coffee> AddCoffee(int Id, string CoffeeName, int Qty, int Price)
        {
            List<Coffee> coffee = GetAll(); // Retrieve the existing list

            // Create a new coffee object with the provided values
            Coffee newCoffee = new Coffee
            {
                Id = Id,
                CoffeeName = CoffeeName,
                Qty = Qty,
                Price = Price
            };

            coffee.Add(newCoffee); // Add the new coffee to the list

            SaveAll(coffee); // Save the updated list to the JSON file

            return coffee;

        }

    }
}
