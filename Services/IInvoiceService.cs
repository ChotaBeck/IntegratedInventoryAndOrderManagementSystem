using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Models;

namespace IntegratedInventoryAndOrderManagementSystem.Services
{
    public interface IInvoiceService
    {
        public Task<Invoice> GenerateInvoiceAsync(int salesOrderId);
    }
}