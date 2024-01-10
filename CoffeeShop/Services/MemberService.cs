using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public class MemberService
    {

        private static void SaveAll(List<Models.Member> member)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetMembersFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(member);
            File.WriteAllText(appUsersFilePath, json);
        }

        //getting back the saved value
        public static List<Models.Member> GetAll()
        {
            string appUsersFilePath = Utils.GetMembersFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<Models.Member>();
            }

            var json = File.ReadAllText(appUsersFilePath);
            return JsonSerializer.Deserialize<List<Models.Member>>(json);
        }

        //adding the vlaue
        public static List<Models.Member> AddMember(int Id, string CustomerName, string CustomerNumber)
        {
            List<Models.Member> member = GetAll(); // Retrieve the existing list

            // Create a new coffee object with the provided values
            Models.Member newMember = new Models.Member
            {
                Id = Id,
                CustomerName = CustomerName,

                CustomerNumber = CustomerNumber
            };

            member.Add(newMember); // Add the new coffee to the list

            SaveAll(member); // Save the updated list to the JSON file

            return member;

        }

        public static List<Models.Member> DeleteMember(int Id)
        {

            List<Models.Member> member = GetAll();
            var memberToDelete = member.FirstOrDefault(c => c.Id == Id);

            if (memberToDelete != null)
            {
                member.Remove(memberToDelete);
                SaveAll(member);

            }

            return member;

        }

    }
}
