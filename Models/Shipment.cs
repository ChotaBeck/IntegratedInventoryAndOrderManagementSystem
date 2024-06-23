using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public DateOnly ShippingDate { get; set; }
        public string TrackingNumber { get; set; }
    }
}