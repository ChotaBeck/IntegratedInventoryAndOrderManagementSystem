using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using IntegratedInventoryAndOrderManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class InventoryController : GenericController<Inventory>
{
    private readonly ILogger<InventoryController> _logger;
    private readonly ApplicationDbContext _context;

    public InventoryController(ILogger<InventoryController> logger, ServiceBase<Inventory> entity, ApplicationDbContext context) : base(logger,entity)
    {
        _context = context;
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
    

    //Get
    public override IActionResult Details(int id)
    {
        var inventory = _entity.GetById(id);
        inventory.Product = _context.Products.Find(inventory.ProductId);
        inventory.Location = _context.Locations.Find(inventory.LocationId);
        return View(inventory);
    }


}
