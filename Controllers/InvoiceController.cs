using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IInvoiceService _invoiceService;
        private readonly InvoicePdfService _pdfService;

        public InvoiceController(ApplicationDbContext context, IInvoiceService invoiceService, InvoicePdfService pdfService)
        {
            _context = context;
            _invoiceService = invoiceService;
            _pdfService = pdfService;
        }
        
        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            return View(await _context.Invoices.Include(i => i.SalesOrder).ToListAsync());
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.SalesOrder)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

     
        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SalesOrderId,InvoiceNumber,InvoiceDate,TotalAmount,Status,DueDate")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(invoice);
        }

        

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }

        // GET: Invoice/Print/5
        public async Task<IActionResult> Print(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.SalesOrder)
                .ThenInclude(so => so.SalesOrderItems)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/MarkAsPaid/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsPaid(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            invoice.Status = InvoiceStatus.Paid;
            _context.Update(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = invoice.Id });
        }

      public async Task<IActionResult> DownloadPdf(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.SalesOrder)
                .ThenInclude(so => so.SalesOrderItems)
                .ThenInclude(soi => soi.Product) // Ensure Product is included
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            if (invoice.SalesOrder == null || invoice.SalesOrder.SalesOrderItems == null)
            {
                return BadRequest("Invoice or Sales Order Items are missing");
            }

            var pdfBytes = _pdfService.GenerateInvoicePdf(invoice, invoice.SalesOrder.SalesOrderItems.ToList());

            return File(pdfBytes, "application/pdf", $"Invoice_{invoice.InvoiceNumber}.pdf");
        }

    }
}