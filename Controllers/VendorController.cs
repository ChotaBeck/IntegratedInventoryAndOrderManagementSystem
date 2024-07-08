using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;


public class VendorController : GenericController<Vendor>
{
    private readonly ILogger<VendorController> _logger;

    public VendorController(ILogger<VendorController> logger, ServiceBase<Vendor> entity) : base(logger,entity)
    {
        
    }
    


}
