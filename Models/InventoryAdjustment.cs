using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class InventoryAdjustment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string AdjustmentType { get; set; }
        public string PerformedBy { get; set; }
        
    }
}