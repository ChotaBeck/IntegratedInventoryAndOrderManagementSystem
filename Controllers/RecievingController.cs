using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

public class RecievingController : Controller
{
    private readonly ApplicationDbContext _context;

    public RecievingController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Receiving
    public async Task<IActionResult> Index()
    {
        var purchaseOrders = await _context.PurchaseOrders.Where(po => !po.IsReceived).ToListAsync();
        foreach (var purchaseOrder in purchaseOrders)
        {
            purchaseOrder.PurchaseOrderItems = await _context.PurchaseOrderItems
                .Where(poi => poi.PurchaseOrderId == purchaseOrder.Id)
                .Include(poi => poi.Product)
                .ToListAsync();
            purchaseOrder.Vendor = await _context.Vendors.FindAsync(purchaseOrder.VendorId);    
            purchaseOrder.Status = await
                _context.Statuses.FindAsync(purchaseOrder.StatusId);
                } 
        return View(purchaseOrders);
    }

    // GET: Receiving/Receive
    [HttpGet]
    public IActionResult Receive(int id)
    {
        var purchaseOrder = _context.PurchaseOrders
            .Where(po => !po.IsReceived)
            .Include(po => po.PurchaseOrderItems)
                .ThenInclude(poi => poi.Product)
            .Include(po => po.Vendor)
            .Include(po => po.Status)
            .FirstOrDefault(po => po.Id == id);  

        if (purchaseOrder == null)
        {
            return NotFound();
        }
        
        var viewModel = new ReceivingViewModel
        {
            PurchaseOrderId = id,
            ReceiveDate = DateTime.Now,
            PurchaseOrderItems = purchaseOrder.PurchaseOrderItems.Select(poi => new RecievePurchaseOrderItemViewModel
            {
                Id = poi.Id,
                ProductId = poi.ProductId,
                ProductName = poi.Product.Name,
                Quantity = poi.Quantity,
                ReceivedQuantity = 0 // Default to 0, or you could set this to poi.ReceivedQuantity if you want to show previously received quantities
            }).ToList()
        };

        return View(viewModel);
    }
    // // GET: Receiving/Recieve
    // [HttpGet]
    // public IActionResult Recieve(int id)
    // {
    //    var purchaseOrder = _context.PurchaseOrders
    //         .Where(po => !po.IsReceived)
    //         .Include(po => po.PurchaseOrderItems)
    //         .Include(po => po.Vendor)
    //         .Include(po => po.Status)
    //         .FirstOrDefault(po => po.Id == id);  

    //     if (purchaseOrder == null)
    //     {
    //         return NotFound();
    //     }
        
    //     var receiving = new Receiving
    //     {
    //         PurchaseOrderId = id,
    //         ReceiveDate = DateTime.Now,
    //         PurchaseOrder = purchaseOrder
    //     };

    //     foreach (var item in purchaseOrder.PurchaseOrderItems)
    //     {
    //         item.Product = _context.Products.Find(item.ProductId);
    //     }
    //     return View(receiving);
    // }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Receive(ReceivingViewModel viewModel)
    {
         // Remove validation for read-only properties
        ModelState.Remove("PurchaseOrderItems");
        for (int i = 0; i < viewModel.PurchaseOrderItems.Count; i++)
        {
            ModelState.Remove($"PurchaseOrderItems[{i}].ProductName");
            ModelState.Remove($"PurchaseOrderItems[{i}].Quantity");
        }
        
        if (ModelState.IsValid)
        {
            var purchaseOrder = await _context.PurchaseOrders
                .Include(po => po.PurchaseOrderItems)
                .FirstOrDefaultAsync(po => po.Id == viewModel.PurchaseOrderId);
            purchaseOrder.StatusId = 5;
            _context.Update(purchaseOrder);

            if (purchaseOrder != null)
            {
                var receiving = new Receiving
                {
                    PurchaseOrderId = viewModel.PurchaseOrderId,
                    ReceiveDate = viewModel.ReceiveDate,
                    ReceivedBy = viewModel.ReceivedBy,
                    Notes = viewModel.Notes,
                    IsCompleted = viewModel.IsCompleted
                };

                // Update the received quantities
                foreach (var item in viewModel.PurchaseOrderItems)
                {
                    var poItem = purchaseOrder.PurchaseOrderItems.FirstOrDefault(poi => poi.Id == item.Id);
                    if (poItem != null)
                    {
                        poItem.ReceivedQuantity = item.ReceivedQuantity;
                        _context.Update(poItem);
                    }
                }

                // Mark as received if completed
                purchaseOrder.IsReceived = viewModel.IsCompleted;

                if (viewModel.IsCompleted)
                {
                    foreach (var item in purchaseOrder.PurchaseOrderItems)
                    {
                        var inventory = await _context.Inventories
                            .FirstOrDefaultAsync(i => i.ProductId == item.ProductId);

                        if (inventory == null)
                        {
                            inventory = new Inventory
                            {
                                ProductId = item.ProductId,
                                Quantity = item.ReceivedQuantity
                            };
                            _context.Inventories.Add(inventory);
                        }
                        else
                        {
                            inventory.Quantity += item.ReceivedQuantity;
                        }
                    }
                }

                _context.Add(receiving);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        // If we got this far, something failed, redisplay form
        return View(viewModel);
    }
    // // POST: Receiving/Recieve
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Recieve(Receiving receiving)
    // {
    //     ModelState.Remove("PurchaseOrder");
    //     ModelState.Remove("PurchaseOrder.Status");
    //     ModelState.Remove("PurchaseOrder.Vendor");
    //     ModelState.Remove("PurchaseOrder.PurchaseOrderItems[{i}].PurchaseOrder");
    //     ModelState.Remove("PurchaseOrder.PurchaseOrderItems[{i}].Product");
    //     if (ModelState.IsValid)
    //     {
    //         var purchaseOrder = await _context.PurchaseOrders
    //             .Include(po => po.PurchaseOrderItems)
    //             .FirstOrDefaultAsync(po => po.Id == receiving.PurchaseOrderId);

    //         if (purchaseOrder != null)
    //         {
    //             // Update the received quantities
    //             foreach (var item in receiving.PurchaseOrder.PurchaseOrderItems)
    //             {
    //                 var poItem = purchaseOrder.PurchaseOrderItems.FirstOrDefault(poi => poi.Id == item.Id);
    //                 if (poItem != null)
    //                 {
    //                     poItem.ReceivedQuantity = item.ReceivedQuantity;
    //                     _context.Update(poItem);
    //                 }
    //             }

    //             receiving.PurchaseOrder = purchaseOrder;

    //             // Mark as received if completed
    //             purchaseOrder.IsReceived = receiving.IsCompleted;

    //             if (receiving.IsCompleted)
    //             {
    //                 foreach (var item in purchaseOrder.PurchaseOrderItems)
    //                 {
    //                     var inventory = await _context.Inventories
    //                         .FirstOrDefaultAsync(i => i.ProductId == item.ProductId);

    //                     if (inventory == null)
    //                     {
    //                         inventory = new Inventory
    //                         {
    //                             ProductId = item.ProductId,
    //                             Quantity = item.ReceivedQuantity
    //                         };
    //                         _context.Inventories.Add(inventory);
    //                     }
    //                     else
    //                     {
    //                         inventory.Quantity += item.ReceivedQuantity;
    //                     }
    //                 }
    //             }

    //             _context.Add(receiving);
    //             await _context.SaveChangesAsync();
    //             return RedirectToAction(nameof(Index));
    //         }
    //     }

    //     ViewBag.PurchaseOrders = _context.PurchaseOrders
    //         .Where(po => !po.IsReceived)
    //         .ToList();
    //     return RedirectToAction("Recieve", new { id = receiving.Id });
    // }

    // GET: Receiving/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var receiving = await _context.Receivings.FindAsync(id);
        if (receiving == null)
        {
            return NotFound();
        }
        ViewBag.PurchaseOrders = _context.PurchaseOrders.ToList();
        return View(receiving);
    }

    // POST: Receiving/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Receiving receiving)
    {
        if (id != receiving.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(receiving);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceivingExists(receiving.Id))
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
        ViewBag.PurchaseOrders = _context.PurchaseOrders.ToList();
        return View(receiving);
    }

    private bool ReceivingExists(int id)
    {
        return _context.Receivings.Any(e => e.Id == id);
    }
}