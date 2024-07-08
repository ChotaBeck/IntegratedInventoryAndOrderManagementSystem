using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrder
        public async Task<IActionResult> Index()
        {
            return View(await _context.PurchaseOrders.ToListAsync());
        }

        // GET: PurchaseOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Vendor)
                .Include(p => p.Status)
                .Include(p => p.PurchaseOrderItems)
                .ThenInclude(poi => poi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Create
        public IActionResult Create()
        {
            ViewBag.Vendors = new SelectList(_context.Vendors, "Id", "Name");
            ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name");
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorId,OrderDate,StatusId,TotalCost")] PurchaseOrder purchaseOrder, List<PurchaseOrderItem> purchaseOrderItems)
        {
             // Remove the validation errors for Product and Status
        ModelState.Remove("Vendor");
        ModelState.Remove("Status");
        ModelState.Remove("PurchaseOrderItems");
        for(int i = 0; i < purchaseOrderItems.Count; i++)
        {
            ModelState.Remove($"PurchaseOrderItems[{i}].Product");
            ModelState.Remove($"PurchaseOrderItems[{i}].PurchaseOrder");
        }
        

        if (ModelState.IsValid)
        {
          
                // Set Product and Status to null to avoid issues
                purchaseOrder.Vendor = null;
                purchaseOrder.Status = null;
                foreach (var item in purchaseOrderItems)
                {
                    item.Product = null;
                    item.PurchaseOrder = null;
                }

                var purchaseOrderDBModel = new PurchaseOrder
                {
                    VendorId = purchaseOrder.VendorId,
                    OrderDate = purchaseOrder.OrderDate,
                    StatusId = purchaseOrder.StatusId,
                    TotalCost = purchaseOrder.TotalCost
                };
                _context.Add(purchaseOrderDBModel);
                await _context.SaveChangesAsync();

                if (purchaseOrderItems != null && purchaseOrderItems.Any())
                {
                    foreach (var item in purchaseOrderItems)
                    {
                        var purchaseOrdeItemrDBModel = new PurchaseOrderItem
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            CostPrice = item.CostPrice,
                            PurchaseOrderId = purchaseOrderDBModel.Id
                        };
                        item.PurchaseOrderId = purchaseOrderDBModel.Id;
                        _context.PurchaseOrderItems.Add(item);
                    }
                    await _context.SaveChangesAsync();
                }

               
                return RedirectToAction(nameof(Index));
            
        }

            ViewBag.Vendors = new SelectList(_context.Vendors, "Id", "Name", purchaseOrder.VendorId);
            ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", purchaseOrder.StatusId);
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(purchaseOrder);
        }

       

        // GET: PurchaseOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.PurchaseOrderItems)
                .ThenInclude(poi => poi.Product)
                .Include(p => p.Vendor)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }
            ViewBag.VendorId = new SelectList(_context.Vendors, "Id", "Name", purchaseOrder.VendorId);
            ViewBag.StatusId = new SelectList(_context.Statuses, "Id", "Name", purchaseOrder.StatusId);
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(purchaseOrder);
        }

        // POST: PurchaseOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VendorId,OrderDate,StatusId,PurchaseOrderItems,TotalCost")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Vendor");
            ModelState.Remove("Status");
            ModelState.Remove("PurchaseOrderItems");
            for(int i = 0; i < purchaseOrder.PurchaseOrderItems.Count; i++)
            {
                ModelState.Remove($"PurchaseOrderItems[{i}].Product");
                ModelState.Remove($"PurchaseOrderItems[{i}].PurchaseOrder");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.Id))
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
            ViewBag.VendorId = new SelectList(_context.Vendors, "Id", "Name", purchaseOrder.VendorId);
            ViewBag.StatusId = new SelectList(_context.Statuses, "Id", "Name", purchaseOrder.StatusId);
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Vendor)
                .Include(p => p.Status)
                .Include(p => p.PurchaseOrderItems)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrders.Any(e => e.Id == id);
        }
    }
}