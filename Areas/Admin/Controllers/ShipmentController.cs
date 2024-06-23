using IntegratedInventoryAndOrderManagementSystem.Controllers;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;

[Area("Admin")]
public class ShipmentController : GenericController<Shipment>
{
    private readonly ILogger<ShipmentController> _logger;

    public ShipmentController(ILogger<ShipmentController> logger, ServiceBase<Shipment> entity) : base(logger,entity)
    {
        
    }
    


}
