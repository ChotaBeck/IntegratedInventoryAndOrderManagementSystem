using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class Customer : Base
    {
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}