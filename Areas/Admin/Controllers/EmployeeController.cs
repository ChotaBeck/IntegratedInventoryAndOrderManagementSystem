using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;
[Area("Admin")]
public class EmployeeController : GenericController<Employee>
{
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ILogger<EmployeeController> logger, ServiceBase<Employee> entity) : base(logger,entity)
    {
        
    }
    


}
