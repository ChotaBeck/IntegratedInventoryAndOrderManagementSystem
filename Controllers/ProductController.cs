using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class ProductController : GenericController<Product>
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, ServiceBase<Product> entity) : base(logger,entity)
    {
        
    }
    


}
