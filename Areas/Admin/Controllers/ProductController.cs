using IntegratedInventoryAndOrderManagementSystem.Controllers;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;

[Area("Admin")]
public class ProductController : GenericController<Product>
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, ServiceBase<Product> entity) : base(logger,entity)
    {
        
    }
    


}
