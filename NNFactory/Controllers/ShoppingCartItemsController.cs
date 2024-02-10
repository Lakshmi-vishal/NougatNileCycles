using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChocolateFactory.Models;

namespace ChocolateFactory.Controllers
{
    public class ShoppingCartItemsController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public ShoppingCartItemsController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: ShoppingCartItems
        public async Task<IActionResult> Index()
        {
            var adventureWorks2022Context = _context.ShoppingCartItems.Include(s => s.Product);
            return View(await adventureWorks2022Context.ToListAsync());
        }

        // GET: ShoppingCartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItems
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.ShoppingCartItemId == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: ShoppingCartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingCartItemId,ShoppingCartId,Quantity,ProductId,DateCreated,ModifiedDate")] ShoppingCartItem shoppingCartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoppingCartItemId,ShoppingCartId,Quantity,ProductId,DateCreated,ModifiedDate")] ShoppingCartItem shoppingCartItem)
        {
            if (id != shoppingCartItem.ShoppingCartItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartItemExists(shoppingCartItem.ShoppingCartItemId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItems
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.ShoppingCartItemId == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(id);
            if (shoppingCartItem != null)
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartItemExists(int id)
        {
            return _context.ShoppingCartItems.Any(e => e.ShoppingCartItemId == id);
        }
    }
}
