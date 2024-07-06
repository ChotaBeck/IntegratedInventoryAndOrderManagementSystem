using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using IntegratedInventoryAndOrderManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

[Authorize(Roles = "Admin")]
public class InventoryController : GenericController<Inventory>
{
    private readonly ILogger<InventoryController> _logger;
    private readonly ApplicationDbContext _context;

    public InventoryController(ILogger<InventoryController> logger, ServiceBase<Inventory> entity, ApplicationDbContext context) : base(logger,entity)
    {
        _context = context;
    }
    //Get

    override public IActionResult Create()
    {
        
        ViewBag.Products = new SelectList(_context.Set<Product>().ToList(), "Id", "Name");
        ViewBag.Locations = new SelectList(_context.Set<Location>().ToList(), "Id", "Name");
        return View();
    }
    override public IActionResult Index()
    {
        var inventories = _entity.GetAll();
        foreach (var inventory in inventories)
        {
            inventory.Product = _context.Products.Find(inventory.ProductId);
            inventory.Location = _context.Locations.Find(inventory.LocationId);
        }
       return View(inventories);
    }
    override public IActionResult Edit(int id)
    {
        ViewBag.Products = new SelectList(_context.Set<Product>().ToList(), "Id", "Name");
        ViewBag.Locations = new SelectList(_context.Set<Location>().ToList(), "Id", "Name");
        return base.Edit(id);
    }

    public override IActionResult Delete(int id)
    {
        ViewBag.Products = new SelectList(_context.Set<Product>().ToList(), "Id", "Name");
        ViewBag.Locations = new SelectList(_context.Set<Location>().ToList(), "Id", "Name");
        return base.Delete(id);
    }

    public override IActionResult Details(int id)
    {
        var inventory = _entity.GetById(id);
        inventory.Product = _context.Products.Find(inventory.ProductId);
        inventory.Location = _context.Locations.Find(inventory.LocationId);
        return View(inventory);
    }


}
