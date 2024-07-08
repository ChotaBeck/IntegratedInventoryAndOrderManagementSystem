using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Data;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderController(ApplicationDbContext context)
        {
            _context = context;
        }
         // GET: SalesOrder
        public async Task<IActionResult> Index()
        {
            var salesOrders = await _context.SalesOrders
                .Include(s => s.Status)
                .OrderByDescending(s => s.OrderDate)
                .ToListAsync();
            return View(salesOrders);
        }

        // GET: SalesOrder/Create
        public IActionResult Create()
        {
            ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name");
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: SalesOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PhoneNumber,Address,OrderDate,StatusId,isPaid,TotalCost")] SalesOrder salesOrder, List<SalesOrderItem> salesOrderItems)
        {
            
            ModelState.Remove("Status");
            ModelState.Remove("SalesOrderItems");
            for(int i = 0; i < salesOrderItems.Count; i++)
            {
                ModelState.Remove($"SalesOrderItems[{i}].Product");
                ModelState.Remove($"SalesOrderItems[{i}].SalesOrder");
            }
           
            salesOrder.TrackingNumber = GenerateRandomString(15);
        
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Add(salesOrder);
                    await _context.SaveChangesAsync();

                    if (salesOrderItems != null && salesOrderItems.Any())
                    {
                        foreach (var item in salesOrderItems)
                        {
                            item.SalesOrderId = salesOrder.Id;
                            _context.SalesOrderItems.Add(item);
                        }
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", salesOrder.StatusId);
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(salesOrder);
        }

           // GET: SalesOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .Include(s => s.Status)
                .Include(s => s.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salesOrder == null)
            {
                return NotFound();
            }

            return View(salesOrder);
        }

         // GET: SalesOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .Include(s => s.SalesOrderItems)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salesOrder == null)
            {
                return NotFound();
            }

            ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", salesOrder.StatusId);
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(salesOrder);
        }

        // POST: SalesOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Address,OrderDate,StatusId,isPaid,TotalCost,ShipDate,TrackingNumber")] SalesOrder salesOrder, List<SalesOrderItem> salesOrderItems)
        {
            if (id != salesOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using var transaction = await _context.Database.BeginTransactionAsync();

                    _context.Update(salesOrder);

                    // Remove existing items not in the updated list
                    var existingItems = await _context.SalesOrderItems.Where(item => item.SalesOrderId == id).ToListAsync();
                    foreach (var existingItem in existingItems)
                    {
                        if (!salesOrderItems.Any(item => item.Id == existingItem.Id))
                        {
                            _context.SalesOrderItems.Remove(existingItem);
                        }
                    }

                    // Update or add items
                    foreach (var item in salesOrderItems)
                    {
                        if (item.Id == 0)
                        {
                            item.SalesOrderId = id;
                            _context.SalesOrderItems.Add(item);
                        }
                        else
                        {
                            _context.Update(item);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderExists(salesOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", salesOrder.StatusId);
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(salesOrder);
        }

         // GET: SalesOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .Include(s => s.Status)
                .Include(s => s.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salesOrder == null)
            {
                return NotFound();
            }

            return View(salesOrder);
        }

        // POST: SalesOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrder = await _context.SalesOrders
                .Include(s => s.SalesOrderItems)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salesOrder == null)
            {
                return NotFound();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.SalesOrderItems.RemoveRange(salesOrder.SalesOrderItems);
                _context.SalesOrders.Remove(salesOrder);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                // Log the error
                return RedirectToAction(nameof(Index), new { error = "Unable to delete the sales order. Please try again." });
            }
        }

        private bool SalesOrderExists(int id)
        {
            return _context.SalesOrders.Any(e => e.Id == id);
        }
        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = characters[random.Next(characters.Length)];
            }

            return new string(result);
        }


    }
}