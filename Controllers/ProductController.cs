using IntegratedInventoryAndOrderManagementSystem.Controllers;
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers;



public class ProductController : GenericController<Product>
{
    private readonly ILogger<ProductController> _logger;
    private readonly ApplicationDbContext _context;

    public ProductController(ILogger<ProductController> logger, ServiceBase<Product> entity, ApplicationDbContext context) : base(logger,entity)
    {
        _context = context;   
    }

    // GET: ProductController
    [HttpGet]
    public override IActionResult Create()
    {
        ViewBag.Locations = new SelectList(_context.Locations, "Id", "Name");
        return View(_entity.GetAll());
    }
    


}
