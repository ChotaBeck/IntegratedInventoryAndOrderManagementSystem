using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class StatusController : GenericController<Status>
{
    private readonly ILogger<StatusController> _logger;

    public StatusController(ILogger<StatusController> logger, ServiceBase<Status> entity) : base(logger,entity)
    {
        
    }
    


}