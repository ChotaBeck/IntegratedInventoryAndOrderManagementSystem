using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        [ForeignKey("SalesOrderId")]
        public SalesOrder SalesOrder { get; set; }
        public DateOnly ShippingDate { get; set; }
        public string TrackingNumber { get; set; }
        public string ShippingCarrier { get; set; }
        public decimal ShippingCost { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        
    }
}