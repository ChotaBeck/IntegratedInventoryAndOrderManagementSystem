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
    public IActionResult Create()
    {
        ViewBag.SalesOrders = new SelectList(_context.SalesOrders, "Id", "Id");
        return View();
    }

    // CREATE POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SalesOrderId,ShippingDate,TrackingNumber")] Shipment shipment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.SalesOrders = new SelectList(_context.SalesOrders, "Id", "Id", shipment.SalesOrderId);
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