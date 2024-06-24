using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;
using Microsoft.AspNetCore.Authorization;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserRoleController : GenericController<UserRole>
{
    private readonly ILogger<UserRoleController> _logger;

    public UserRoleController(ILogger<UserRoleController> logger, ServiceBase<UserRole> entity) : base(logger,entity)
    {
        
    }
    


}
