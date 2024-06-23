using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;

[Area("Admin")]
public class VendorController : GenericController<Vendor>
{
    private readonly ILogger<VendorController> _logger;

    public VendorController(ILogger<VendorController> logger, ServiceBase<Vendor> entity) : base(logger,entity)
    {
        
    }
    


}
