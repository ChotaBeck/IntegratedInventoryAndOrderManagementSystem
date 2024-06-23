using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class SalesOrderController : GenericController<SalesOrder>
{
    private readonly ILogger<SalesOrderController> _logger;

    public SalesOrderController(ILogger<SalesOrderController> logger, ServiceBase<SalesOrder> entity) : base(logger,entity)
    {
        
    }
    


}
