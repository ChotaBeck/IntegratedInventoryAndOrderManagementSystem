using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }
        public DateOnly OrderDate { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public double TotalCost { get; set; }

    }
}