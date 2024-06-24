using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.Stock.Controllers;

[Area("Stock")]
public class RecievingController : GenericController<PurchaseOrder>
{
    private readonly ILogger<RecievingController> _logger;

    public RecievingController(ILogger<RecievingController> logger, ServiceBase<PurchaseOrder> entity) : base(logger,entity)
    {
        
    }
    


}
