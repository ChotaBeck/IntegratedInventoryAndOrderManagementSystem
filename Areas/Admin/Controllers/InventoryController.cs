using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;
[Area("Admin")]
public class InventoryController : GenericController<Inventory>
{
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(ILogger<InventoryController> logger, ServiceBase<Inventory> entity) : base(logger,entity)
    {
        
    }
    


}
