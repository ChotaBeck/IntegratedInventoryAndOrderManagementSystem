using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.Stock.Controllers;

[Area("Stock")]
public class StockClerkController : GenericController<Inventory>
{
    private readonly ILogger<StockClerkController> _logger;

    public StockClerkController(ILogger<StockClerkController> logger, ServiceBase<Inventory> entity) : base(logger,entity)
    {
        
    }
    


}
