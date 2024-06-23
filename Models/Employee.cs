using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class Employee : User
    {
        public string Postion { get; set; }
        public string ManNumber { get; set; }
        public string Department { get; set; }
    }
}