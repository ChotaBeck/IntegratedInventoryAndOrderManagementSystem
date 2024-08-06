using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegratedInventoryAndOrderManagementSystem.Services
{
    public class InvoiceService : IInvoiceService
{
    private readonly ApplicationDbContext _context;

    public InvoiceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Invoice> GenerateInvoiceAsync(int salesOrderId)
    {
        var salesOrder = await _context.SalesOrders
            .Include(so => so.SalesOrderItems)
            .FirstOrDefaultAsync(so => so.Id == salesOrderId);

        if (salesOrder == null)
        {
            throw new ArgumentException("Sales order not found");
        }

        var invoice = new Invoice
        {
            SalesOrderId = salesOrderId,
            InvoiceNumber = GenerateInvoiceNumber(),
            InvoiceDate = DateTime.Now,
            TotalAmount = salesOrder.SalesOrderItems.Sum(item => item.Quantity * item.SellingPrice),
            Status = InvoiceStatus.Unpaid,
            DueDate = DateTime.Now.AddDays(30) // Set due date to 30 days from now
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return invoice;
    }

       

        private string GenerateInvoiceNumber()
    {
        // Implement your logic to generate a unique invoice number
        // For example: INV-YYYYMMDD-XXXX
        return $"INV-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper()}";
    }
}
}