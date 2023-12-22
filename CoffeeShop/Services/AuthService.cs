using System.Collections.Generic;
using System.Linq;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Components;

namespace CoffeeShop.Services
{
    public class AuthService
    {
        private List<Users> users = new List<Users>();

        public AuthService()
        {
            // Add an admin user for demonstration purposes
            users.Add(new Users { Username = "admin", PasswordHash = "adminpassword" });
        }

        public bool RegisterUser(string username, string password)
        {
            // Check if the username is unique (you may want to improve this check)
            if (users.Any(u => u.Username == username))
            {
                return false; // Username already exists
            }

            // Hash the password (use a proper password hashing library in a real-world scenario)
            string passwordHash = HashPassword(password);

            // Add the user to the list
            users.Add(new Users { Username = username, PasswordHash = passwordHash });

            return true;
        }

        public bool Login(string username, string password)
        {
            // Find the user by username
            var user = users.FirstOrDefault(u => u.Username == username);  

            if (user != null)
            {
                // Check the password (use a proper password hashing library in a real-world scenario)
                if (VerifyPassword(password, user.PasswordHash))
                {
                    return true; // Login successful
                }
            }

            return false; // Login failed
        }

        public void RedirectToPage(NavigationManager navigationManager, string page)
        {
            navigationManager.NavigateTo(page);
        }

        private string HashPassword(string password)
        {
            // Implement a proper password hashing algorithm (e.g., BCrypt, Argon2)
            // For simplicity, you might want to use a library for this purpose.
            return password; // For demonstration purposes, we'll just return the plain password.
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Implement password verification logic (e.g., compare hashed passwords)
            // For simplicity, we'll just compare plain passwords.
            return enteredPassword == storedPasswordHash;
        }
    }
}
