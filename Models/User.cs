using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class User : Base
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId{ get; set; } =2;
        [ForeignKey("RoleId")]
        public UserRole UserRole { get; set; }
        
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        

    }
}