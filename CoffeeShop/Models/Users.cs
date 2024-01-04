using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Models
{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public bool HasInitialPassword { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }

    }

  

}
