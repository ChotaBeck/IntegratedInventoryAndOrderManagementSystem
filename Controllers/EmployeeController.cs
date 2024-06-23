using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;


namespace IntegratedInventoryAndOrderManagementSystem.Controllers;

public class EmployeeController : GenericController<Employee>
{
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(ILogger<EmployeeController> logger, ServiceBase<Employee> entity) : base(logger,entity)
    {
        
    }
    


}
