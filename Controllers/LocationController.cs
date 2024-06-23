using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class LocationController : GenericController<Location>
{
    private readonly ILogger<LocationController> _logger;

    public LocationController(ILogger<LocationController> logger, ServiceBase<Location> entity) : base(logger,entity)
    {
        
    }
    


}
