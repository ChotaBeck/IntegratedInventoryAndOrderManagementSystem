using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCounts()
        {
            var productCount = await _context.Products.CountAsync();
            var orderCount = await _context.SalesOrders.CountAsync();
            var shipmentCount = await _context.Shipments.CountAsync();
            var customerCount = await _context.Customers.CountAsync();

            return Json(new
            {
                productCount,
                orderCount,
                shipmentCount,
                customerCount
            });
        }
    }
}
