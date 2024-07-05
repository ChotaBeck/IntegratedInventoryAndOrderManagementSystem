using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class InventoryAdjustment
    {
        public int Id { get; set; }
        int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        string AdjustmentType { get; set; }
        public string PerformedBy { get; set; }
        
    }
}