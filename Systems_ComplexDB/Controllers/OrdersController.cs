using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Systems_ComplexDB.Data;
using Systems_ComplexDB.Models;

namespace Systems_ComplexDB.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                Product yo = _context.Product.Where(x => x.ProductName == order.ProductName).FirstOrDefault();
                String name = yo.ProductName;
                if (name.Equals(order.ProductName))
                {
                    Inventory Inv = _context.Inventory.Where(x => x.Product.ProductName == order.ProductName).FirstOrDefault();
                    Inv.Stock += order.Quantity;
                    Inv.LastUpdated = DateTime.Now;
                    Inv.Product = _context.Product.Where(x => x.ProductName == order.ProductName).FirstOrDefault();
                    Inv.Product.ProductName = order.ProductName;
                    Inv.Product.Price = order.Price;

                    _context.Inventory.Update(Inv);
                    _context.Product.Update(Inv.Product);
                }
                else
                {
                    Product Prod = new()
                    {
                        ProductName = order.ProductName,
                        Price = order.Price
                    };

                    Inventory Inv = new()
                    {
                        Stock = order.Quantity,
                        LastUpdated = DateTime.Now,
                        Product = Prod

                    };

                    _context.Inventory.Add(Inv);
                    _context.Product.Add(Inv.Product);
                }

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,SupplierName,OrderedAt,ProductName,Price,Quantity")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Inventory Inv = _context.Inventory.Where(x => x.Product.ProductName == order.ProductName).FirstOrDefault();
                    Inv.Product = _context.Product.Where(x => x.ProductName == order.ProductName).FirstOrDefault();
                    Order ord = _context.Order.Where(x => x.OrderID == order.OrderID).AsNoTracking().FirstOrDefault();

                    if (ord.Quantity > order.Quantity)
                    {
                        Inv.Stock += ord.Quantity - order.Quantity;
                    }
                    else
                    {
                        Inv.Stock -= order.Quantity - ord.Quantity;
                    }


                    Inv.LastUpdated = DateTime.Now;

                    Inv.Product.ProductName = order.ProductName;
                    Inv.Product.Price = order.Price;

                    _context.Update(order);                    
                    _context.Inventory.Update(Inv);
                    _context.Product.Update(Inv.Product);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
