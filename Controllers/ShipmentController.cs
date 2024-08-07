using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

// [Authorize(Roles = "Admin")]
public class ShipmentController : Controller
{
    private readonly ApplicationDbContext _context;

    public ShipmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // INDEX
    public async Task<IActionResult> Index()
    {
        var shipments = await _context.Shipments
            .Include(s => s.SalesOrder)
            .ToListAsync();
        return View(shipments);
    }

    // DETAILS
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shipment = await _context.Shipments
            .Include(s => s.SalesOrder)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (shipment == null)
        {
            return NotFound();
        }

        return View(shipment);
    }

    // CREATE GET
public IActionResult Create(int id)
{
    var salesOrder = _context.SalesOrders.Find(id);
    if (salesOrder == null)
    {
        return NotFound();
    }

    ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name");
    ViewBag.SalesOrderId = new SelectList(new[] { salesOrder }, "Id", "Id");

    var shipment = new Shipment
    {
        SalesOrderId = salesOrder.Id,
        ShippingDate = DateOnly.FromDateTime(DateTime.Now),
        TrackingNumber = salesOrder.TrackingNumber
    };
    return View(shipment);
}

    public async Task<IActionResult> NewShipment()
    {
        var salesOrders = await _context.SalesOrders
                .Include(s => s.Status)
                .OrderByDescending(s => s.OrderDate)
                .Where(s => s.ShipDate == null)
                .ToListAsync();
                
            return View(salesOrders);

    }

   // CREATE POST
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("SalesOrderId,ShippingDate,TrackingNumber,ShippingCarrier,ShippingCost,StatusId")] Shipment shipment)
{
    ModelState.Remove("SalesOrder"); // Remove complex type validation
    ModelState.Remove("Status");
    if (ModelState.IsValid)
    {
        try
        {
            var salesOrder = await _context.SalesOrders.FindAsync(shipment.SalesOrderId);
            salesOrder.ShipDate = DateTime.Now;
            if (salesOrder == null)
            {
                return NotFound($"Sales Order with ID {shipment.SalesOrderId} not found.");
            }
            

            // Update the sales order status if needed
            salesOrder.StatusId = shipment.StatusId; // Assuming you want to update the sales order status
            _context.Update(salesOrder);

            _context.Add(shipment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Unable to create shipment. Error: {ex.Message}");
        }
    }

    // If we got this far, something failed; redisplay form
    ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", shipment.StatusId);
    ViewBag.SalesOrderId = new SelectList(new[] { await _context.SalesOrders.FindAsync(shipment.SalesOrderId) }, "Id", "Id");
    return View(shipment);
}

    // EDIT GET
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }
        ViewBag.SalesOrders = new SelectList(_context.SalesOrders, "Id", "Id", shipment.SalesOrderId);
        return View(shipment);
    }

    // EDIT POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,SalesOrderId,ShippingDate,TrackingNumber")] Shipment shipment)
    {
        if (id != shipment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(shipment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentExists(shipment.Id))
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
        ViewBag.SalesOrders = new SelectList(_context.SalesOrders, "Id", "Id", shipment.SalesOrderId);
        return View(shipment);
    }

    // DELETE GET
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shipment = await _context.Shipments
            .Include(s => s.SalesOrder)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (shipment == null)
        {
            return NotFound();
        }

        return View(shipment);
    }

    // DELETE POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ShipmentExists(int id)
    {
        return _context.Shipments.Any(e => e.Id == id);
    }
}