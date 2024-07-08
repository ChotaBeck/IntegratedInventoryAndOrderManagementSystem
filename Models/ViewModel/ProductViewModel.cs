using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntegratedInventoryAndOrderManagementSystem.Models.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public double Cost { get; set; }
        [Required]
        public int LocationId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime LastRestockDate { get; set; }
        public int ReorderThreshold { get; set; }

        public Location Location { get; set; }
    }
}