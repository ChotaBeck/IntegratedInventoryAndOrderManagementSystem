using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;

[Area("Admin")]
public class SalesOrderController : GenericController<SalesOrder>
{
    private readonly ILogger<SalesOrderController> _logger;

    public SalesOrderController(ILogger<SalesOrderController> logger, ServiceBase<SalesOrder> entity) : base(logger,entity)
    {
        
    }
    


}
