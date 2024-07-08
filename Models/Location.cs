using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class Location : Base
    {
        
    // Collection of Products
    public virtual List<Product> Products { get; set; }
        
    }
}