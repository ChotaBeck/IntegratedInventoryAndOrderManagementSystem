using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public DateOnly OrderDate { get; set; }
        public string Status { get; set; }
        
    }
}