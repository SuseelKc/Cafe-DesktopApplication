using CoffeeShop.Components.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public class AddinService
    {

        private static void SaveAll(List<Models.Addins> addins)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetAddinFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(addins);
            File.WriteAllText(appUsersFilePath, json);
        }

        //getting back the saved value
        public static List<Models.Addins> GetAll()
        {
            string appUsersFilePath = Utils.GetAddinFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<Models.Addins>();
            }

            var json = File.ReadAllText(appUsersFilePath);
            return JsonSerializer.Deserialize<List<Models.Addins>>(json);
        }

        //adding the vlaue
        public static List<Models.Addins> AddAddins(int Id, string CoffeeName, int Price)
        {
            List<Models.Addins> addins = GetAll(); // Retrieve the existing list

            // Create a new coffee object with the provided values
            Models.Addins newAddins = new Models.Addins
            {
                Id = Id,
                Name = CoffeeName,

                Price = Price
            };

            addins.Add(newAddins); // Add the new coffee to the list

            SaveAll(addins); // Save the updated list to the JSON file

            return addins;

        }

        public static List<Models.Addins> DeleteAddins(int Id)
        {

            List<Models.Addins> addins = GetAll();
            var addinsToDelete = addins.FirstOrDefault(c => c.Id == Id);

            if (addinsToDelete != null)
            {
                addins.Remove(addinsToDelete);
                SaveAll(addins);

            }

            return addins;

        }

    }
}



