using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly OrderDate { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public List<SalesOrderItem> SalesOrderItems { get; set; }
        public bool isPaid { get; set; } = false;
        public decimal TotalCost { get; set; }
        public DateTime? ShipDate { get; set; }
        public string? TrackingNumber { get; set; }
        public object OrderItems { get; internal set; }
    }
}