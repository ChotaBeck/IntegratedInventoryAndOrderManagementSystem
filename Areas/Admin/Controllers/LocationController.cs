using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;
using Microsoft.AspNetCore.Authorization;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class LocationController : GenericController<Location>
{
    private readonly ILogger<LocationController> _logger;

    public LocationController(ILogger<LocationController> logger, ServiceBase<Location> entity) : base(logger,entity)
    {
        
    }
    


}
