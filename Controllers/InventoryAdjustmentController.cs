using System;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class InventoryAdjustmentController : Controller
{
    private readonly ApplicationDbContext _context;

    public InventoryAdjustmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: InventoryAdjustment/Create
    public IActionResult Create()
    {
        ViewBag.Products = _context.Products.ToList();
        return View();
    }

    // POST: InventoryAdjustment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InventoryAdjustment inventoryAdjustment)
    {
        ModelState.Remove("Product");
        
        if (ModelState.IsValid)
        {
            inventoryAdjustment.Date = DateTime.Now;
            _context.Add(inventoryAdjustment);

            // Adjust the inventory
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == inventoryAdjustment.ProductId);

            if (inventory == null)
            {
                return NotFound();
            }
            else
            {
                // Update existing inventory
                inventory.Quantity = inventoryAdjustment.Quantity;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Products = _context.Products.ToList();
        return View(inventoryAdjustment);
    }

    // GET: InventoryAdjustment/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inventoryAdjustment = await _context.InventoryAdjustments
            .Include(i => i.Product)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (inventoryAdjustment == null)
        {
            return NotFound();
        }

        return View(inventoryAdjustment);
    }

    //GET
    public IActionResult Index()
    {
        var inventoryAdjustment = _context.Set<InventoryAdjustment>().ToList();
        return View(inventoryAdjustment);
    }
}