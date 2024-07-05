using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class Product : Base
    {
        public string Description { get; set; }
        public string SKU { get; set; }
        public double Cost { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location location {get;set;}
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime LastRestockDate { get; set; }
        public int ReorderThreshold { get; set; }
    }
}