using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;
using IntegratedInventoryAndOrderManagementSystem.Controllers;
using Microsoft.AspNetCore.Authorization;


namespace IntegratedInventoryAndOrderManagementSystem.Areas.admin.Controllers;

 [Area("Admin")]
 [Authorize(Roles = "Admin")]
public class CustomerController : GenericController<Customer>
{
    private readonly ILogger<CustomerController> _logger;
   

    public CustomerController(ILogger<CustomerController> logger, ServiceBase<Customer> entity) : base(logger,entity)
    {
        
    }
    


}
