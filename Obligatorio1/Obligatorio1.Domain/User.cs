using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public List<Purchase>? Purchases { get; set; }
        public Cart? Cart { get; set; }
        public User()
        {
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.Email = string.Empty;
            this.Address = string.Empty;
            this.Role = string.Empty;
            this.Purchases = null;
            this.Cart = new Cart { Products = new List<Product>() };
        }

        public User(int userID, string userName, string password, string email, string address, string role, List<Purchase>? purchases)
        {
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Role = role;
            this.Purchases = purchases;
            this.Cart = new Cart { Products = new List<Product>() };
        }
    }
}