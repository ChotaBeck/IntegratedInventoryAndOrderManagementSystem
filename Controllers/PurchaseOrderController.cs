using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class PurchaseOrderController : GenericController<PurchaseOrder>
{
    private readonly ILogger<PurchaseOrderController> _logger;

    public PurchaseOrderController(ILogger<PurchaseOrderController> logger, ServiceBase<PurchaseOrder> entity) : base(logger,entity)
    {
        
    }
    


}
