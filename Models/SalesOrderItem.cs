using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models
{
    public class SalesOrderItem
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        [ForeignKey("SalesOrderId")]
        public SalesOrder SalesOrder { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double SellingPrice { get; set; }

    }
}