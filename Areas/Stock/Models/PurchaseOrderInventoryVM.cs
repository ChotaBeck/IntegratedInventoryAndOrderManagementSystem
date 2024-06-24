using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Models;

namespace IntegratedInventoryAndOrderManagementSystem.Areas.Stock.Models
{
    public class PurchaseOrderInventoryVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int Quantity { get; set; }
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