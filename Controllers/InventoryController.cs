using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class InventoryController : GenericController<Inventory>
{
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger, ServiceBase<Inventory> entity) : base(logger,entity)
    {
        
    }
    


}
