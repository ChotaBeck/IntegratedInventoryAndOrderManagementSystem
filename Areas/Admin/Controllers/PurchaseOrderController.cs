using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;
using IntegratedInventoryAndOrderManagementSystem.Models.ViewModel;
using IntegratedInventoryAndOrderManagementSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PurchaseOrderController : Controller
{
    private readonly ILogger<PurchaseOrderController> _logger;
    private readonly ApplicationDbContext _context;

    public PurchaseOrderController(ILogger<PurchaseOrderController> logger, ApplicationDbContext context)
    {
        _context = context;
    }
    //get
    public IActionResult Create()
    {
        ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name");
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
    
        var viewModel = new CreatePurchaseOrderViewModel
        {
            OrderDate = DateOnly.FromDateTime(DateTime.Today)
        };
        return View(viewModel);
    }

    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreatePurchaseOrderViewModel model)
    {
        if (ModelState.IsValid)
    {
        var purchaseOrder = new PurchaseOrder
        {
            Id = model.CustomerId,
            OrderDate = model.OrderDate,
            StatusId = model.StatusId,
            PurchaseOrderItems = model.Items.Select(item => new PurchaseOrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                CostPrice = item.UnitPrice
            }).ToList()
        };

        // Calculate total cost
        purchaseOrder.TotalCost = purchaseOrder.PurchaseOrderItems.Sum(item => item.Quantity * item.CostPrice);

        // Save to database
        _context.PurchaseOrders.Add(purchaseOrder);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    return View(model);
    }
    

    // INDEX
    public async Task<IActionResult> Index()
    {
        var purchaseOrders = await _context.PurchaseOrders
            .Include(po => po.Status)
            .Include(po => po.PurchaseOrderItems)
            .ToListAsync();
        return View(purchaseOrders);
    }

    // DETAILS
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var purchaseOrder = await _context.PurchaseOrders
            .Include(po => po.Status)
            .Include(po => po.PurchaseOrderItems)
            .FirstOrDefaultAsync(po => po.Id == id);

        if (purchaseOrder == null)
        {
            return NotFound();
        }

        return View(purchaseOrder);
    }

    // EDIT GET
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var purchaseOrder = await _context.PurchaseOrders
            .Include(po => po.PurchaseOrderItems)
            .FirstOrDefaultAsync(po => po.Id == id);

        if (purchaseOrder == null)
        {
            return NotFound();
        }

        ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", purchaseOrder.StatusId);
        return View(purchaseOrder);
    }

    // EDIT POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PurchaseOrder purchaseOrder)
    {
        if (id != purchaseOrder.Id)
        {
            return NotFound();
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
        ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", purchaseOrder.StatusId);
        return View(purchaseOrder);
    }

    // DELETE GET
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var purchaseOrder = await _context.PurchaseOrders
            .Include(po => po.Status)
            .FirstOrDefaultAsync(po => po.Id == id);

        if (purchaseOrder == null)
        {
            return NotFound();
        }

        return View(purchaseOrder);
    }

    // DELETE POST
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



