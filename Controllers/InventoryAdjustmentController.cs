using System;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
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
        ViewBag.AdjustmentTypes = Enum.GetValues(typeof(AdjustmentType)).Cast<AdjustmentType>().ToList();
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
            // var user = await _userManager.GetUserAsync(User);
            // inventoryAdjustment.PerformedBy = user.UserName;
            _context.Add(inventoryAdjustment);

            // Adjust the inventory
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == inventoryAdjustment.ProductId);

            if (inventory == null)
            {
                inventory = new Inventory
                {
                    ProductId = inventoryAdjustment.ProductId,
                    Quantity = 0
                };
                _context.Inventories.Add(inventory);
            }
            switch (inventoryAdjustment.AdjustmentType)
            {
                case AdjustmentType.NewProducts:
                    inventory.Quantity += inventoryAdjustment.Quantity;
                    break;
                case AdjustmentType.ReturnedOrders:
                    inventory.Quantity += inventoryAdjustment.Quantity;
                    break;
                case AdjustmentType.CancelledOrders:
                    inventory.Quantity += inventoryAdjustment.Quantity;
                    break;
                case AdjustmentType.StockTake:
                    inventory.Quantity = inventoryAdjustment.Quantity;
                    break;
                case AdjustmentType.DamagedProducts:
                    inventory.Quantity -= inventoryAdjustment.Quantity;
                    break;
                case AdjustmentType.OrderFullfillment:
                    inventory.Quantity -= inventoryAdjustment.Quantity;
                    break;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.AdjustmentTypes = Enum.GetValues(typeof(AdjustmentType)).Cast<AdjustmentType>().ToList();
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