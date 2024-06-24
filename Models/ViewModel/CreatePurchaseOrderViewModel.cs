using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models.ViewModel
{
    public class CreatePurchaseOrderViewModel
    {
        public int CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        public int StatusId { get; set; }
        public List<PurchaseOrderItemViewModel> Items { get; set; } = new List<PurchaseOrderItemViewModel>();
    }
    
}