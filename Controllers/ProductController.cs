using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _context.Set<Product>().ToListAsync();
            return View(products);
        }

        // GET: Product/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Set<Product>().Find(id);
            var location = _context.Set<Location>().Find(product.LocationId);
            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                Cost = product.Cost,
                Location = location,
                Price = product.Price,
                Quantity = product.Quantity,
                LastRestockDate = product.LastRestockDate,
                ReorderThreshold = product.ReorderThreshold
            };
            if (product == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.Locations = new SelectList(_context.Locations, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            
            
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                var inventory = new Inventory
                {
                    // Product = product,
                    // Location = _context.Locations.Find(product.LocationId),
                    ProductId = product.Id,
                    LocationId = product.LocationId,
                    Quantity = product.Quantity
                };
                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }


            ViewBag.Locations = new SelectList(_context.Locations, "Id", "Name", product.LocationId);
            return View(product);
        }

        // GET: Product/Edit/5

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Locations = new SelectList(_context.Locations, "Id", "Name", product.LocationId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Product product)
        {


            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewBag.Locations = new SelectList(_context.Locations, "Id", "Name", product.LocationId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            var location = await _context.Locations.FindAsync(product.LocationId);
            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                Cost = product.Cost,
                Location = location,
                Price = product.Price,
                Quantity = product.Quantity,
                LastRestockDate = product.LastRestockDate,
                ReorderThreshold = product.ReorderThreshold
            };
            if (product == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == id);

            _context.Inventories.Remove(inventory);
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
