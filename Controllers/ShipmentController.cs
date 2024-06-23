using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class ShipmentController : GenericController<Shipment>
{
    private readonly ILogger<ShipmentController> _logger;

    public ShipmentController(ILogger<ShipmentController> logger, ServiceBase<Shipment> entity) : base(logger,entity)
    {
        
    }
    


}
