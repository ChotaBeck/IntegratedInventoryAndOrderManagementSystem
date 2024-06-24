using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Models.ViewModel
{
    public class SalesOrderViewModel
    {
        public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateOnly OrderDate { get; set; }
    public int StatusId { get; set; }
    public bool isPaid { get; set; }
    public List<SalesOrderItemViewModel> SalesOrderItems { get; set; } = new List<SalesOrderItemViewModel>();
}
    }
