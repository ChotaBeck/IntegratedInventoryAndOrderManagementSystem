using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class Product : Base
    {
        public string Description { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}