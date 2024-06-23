using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class User : Base
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        

    }
}