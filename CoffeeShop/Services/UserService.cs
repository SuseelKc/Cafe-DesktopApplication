using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Components;
using Windows.System;

namespace CoffeeShop.Services
{
    public static class UserService
    {

        public const string SeedUsername = "admin";
        public const string SeedPassword = "admin";

        private static void SaveAll(List<Users> users)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetAppUsersFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(appUsersFilePath, json);
        }

        public static List<Users> GetAll()
        {
            string appUsersFilePath = Utils.GetAppUsersFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<Users>();
            }

            var json = File.ReadAllText(appUsersFilePath);
            return JsonSerializer.Deserialize<List<Users>>(json);
        }

        public static List<Users> Create(Guid userId, string username, string password, Role role)
        {
            List<Users> users = GetAll();
            bool usernameExists = users.Any(x => x.Username == username);

            if (usernameExists)
            {
                throw new Exception("Username already exists.");
            }

            users.Add(
                new Users
                {
                    Username = username,
                    PasswordHash = Utils.HashSecret(password),
                    Role = role,
                    CreatedBy = userId
                }
            );
            SaveAll(users);
            return users;
        }

        public static void SeedUsers()
        {
            var users = GetAll().FirstOrDefault(x => x.Role == Role.Admin);

            if (users == null)
            {
                Create(Guid.Empty, SeedUsername, SeedPassword, Role.Admin);
            }
        }

        //
        public static Users Login(string username, string password)
        {
            var loginErrorMessage = "Invalid username or password.";
            List<Users> users = GetAll();
            Users user = users.FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                throw new Exception(loginErrorMessage);
            }

            bool passwordIsValid = Utils.VerifyHash(password, user.PasswordHash);

            if (!passwordIsValid)
            {
                throw new Exception(loginErrorMessage);
            }

            return user;
        }


        //
        public static Users ChangePassword(Guid id, string currentPassword, string newPassword)
        {
            if (currentPassword == newPassword)
            {
                throw new Exception("New password must be different from current password.");
            }

            List<Users> users = GetAll();
            Users user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            bool passwordIsValid = Utils.VerifyHash(currentPassword, user.PasswordHash);

            if (!passwordIsValid)
            {
                throw new Exception("Incorrect current password.");
            }

            user.PasswordHash = Utils.HashSecret(newPassword);
            user.HasInitialPassword = false;
            SaveAll(users);

            return user;
        }

    }
}
