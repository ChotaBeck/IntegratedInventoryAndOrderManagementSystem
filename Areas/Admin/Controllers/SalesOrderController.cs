using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SalesOrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public SalesOrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // INDEX
    public async Task<IActionResult> Index()
    {
        var salesOrders = await _context.SalesOrders
            .Include(so => so.Status)
            .ToListAsync();
        return View(salesOrders);
    }

    // DETAILS
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salesOrder = await _context.SalesOrders
            .Include(so => so.Status)
            .Include(so => so.SalesOrderItems)
                .ThenInclude(soi => soi.Product)
            .FirstOrDefaultAsync(so => so.Id == id);

        if (salesOrder == null)
        {
            return NotFound();
        }

        return View(salesOrder);
    }

    // CREATE GET
    public IActionResult Create()
    {
        ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name");
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
        return View(new SalesOrderViewModel());
    }

    // CREATE POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SalesOrderViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var salesOrder = new SalesOrder
            {
                CustomerId = viewModel.CustomerId,
                OrderDate = viewModel.OrderDate,
                StatusId = viewModel.StatusId,
                isPaid = viewModel.isPaid,
                SalesOrderItems = viewModel.SalesOrderItems.Select(item => new SalesOrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SellingPrice = item.SellingPrice
                }).ToList()
            };

            salesOrder.TotalCost = salesOrder.SalesOrderItems.Sum(item => item.Quantity * item.SellingPrice);

            _context.Add(salesOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", viewModel.StatusId);
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
        return View(viewModel);
    }

    // EDIT GET
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salesOrder = await _context.SalesOrders
            .Include(so => so.SalesOrderItems)
            .FirstOrDefaultAsync(so => so.Id == id);

        if (salesOrder == null)
        {
            return NotFound();
        }

        var viewModel = new SalesOrderViewModel
        {
            Id = salesOrder.Id,
            CustomerId = salesOrder.CustomerId,
            OrderDate = salesOrder.OrderDate,
            StatusId = salesOrder.StatusId,
            isPaid = salesOrder.isPaid,
            SalesOrderItems = salesOrder.SalesOrderItems.Select(item => new SalesOrderItemViewModel
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                SellingPrice = item.SellingPrice
            }).ToList()
        };

        ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", salesOrder.StatusId);
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
        return View(viewModel);
    }

    // EDIT POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SalesOrderViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var salesOrder = await _context.SalesOrders
                    .Include(so => so.SalesOrderItems)
                    .FirstOrDefaultAsync(so => so.Id == id);

                if (salesOrder == null)
                {
                    return NotFound();
                }

                salesOrder.CustomerId = viewModel.CustomerId;
                salesOrder.OrderDate = viewModel.OrderDate;
                salesOrder.StatusId = viewModel.StatusId;
                salesOrder.isPaid = viewModel.isPaid;

                // Remove existing items
                _context.SalesOrderItems.RemoveRange(salesOrder.SalesOrderItems);

                // Add updated items
                salesOrder.SalesOrderItems = viewModel.SalesOrderItems.Select(item => new SalesOrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SellingPrice = item.SellingPrice
                }).ToList();

                salesOrder.TotalCost = salesOrder.SalesOrderItems.Sum(item => item.Quantity * item.SellingPrice);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderExists(viewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Statuses = new SelectList(_context.Statuses, "Id", "Name", viewModel.StatusId);
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
        return View(viewModel);
    }

    // DELETE GET
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salesOrder = await _context.SalesOrders
            .Include(so => so.Status)
            .FirstOrDefaultAsync(so => so.Id == id);

        if (salesOrder == null)
        {
            return NotFound();
        }

        return View(salesOrder);
    }

    // DELETE POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var salesOrder = await _context.SalesOrders
            .Include(so => so.SalesOrderItems)
            .FirstOrDefaultAsync(so => so.Id == id);

        _context.SalesOrderItems.RemoveRange(salesOrder.SalesOrderItems);
        _context.SalesOrders.Remove(salesOrder);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SalesOrderExists(int id)
    {
        return _context.SalesOrders.Any(e => e.Id == id);
    }
}