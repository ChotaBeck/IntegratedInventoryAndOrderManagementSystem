using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class StatusController : GenericController<Status>
{
    private readonly ILogger<StatusController> _logger;

    public StatusController(ILogger<StatusController> logger, ServiceBase<Status> entity) : base(logger,entity)
    {
        
    }
    


}
