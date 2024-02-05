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
    public class TransactionHistoryArchivesController : Controller
    {
        private readonly AdventureWorks2022Context _context;

        public TransactionHistoryArchivesController(AdventureWorks2022Context context)
        {
            _context = context;
        }

        // GET: TransactionHistoryArchives
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactionHistoryArchives.ToListAsync());
        }

        // GET: TransactionHistoryArchives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistoryArchive = await _context.TransactionHistoryArchives
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionHistoryArchive == null)
            {
                return NotFound();
            }

            return View(transactionHistoryArchive);
        }

        // GET: TransactionHistoryArchives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionHistoryArchives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,ProductId,ReferenceOrderId,ReferenceOrderLineId,TransactionDate,TransactionType,Quantity,ActualCost,ModifiedDate")] TransactionHistoryArchive transactionHistoryArchive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionHistoryArchive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionHistoryArchive);
        }

        // GET: TransactionHistoryArchives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistoryArchive = await _context.TransactionHistoryArchives.FindAsync(id);
            if (transactionHistoryArchive == null)
            {
                return NotFound();
            }
            return View(transactionHistoryArchive);
        }

        // POST: TransactionHistoryArchives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,ProductId,ReferenceOrderId,ReferenceOrderLineId,TransactionDate,TransactionType,Quantity,ActualCost,ModifiedDate")] TransactionHistoryArchive transactionHistoryArchive)
        {
            if (id != transactionHistoryArchive.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionHistoryArchive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionHistoryArchiveExists(transactionHistoryArchive.TransactionId))
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
            return View(transactionHistoryArchive);
        }

        // GET: TransactionHistoryArchives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistoryArchive = await _context.TransactionHistoryArchives
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transactionHistoryArchive == null)
            {
                return NotFound();
            }

            return View(transactionHistoryArchive);
        }

        // POST: TransactionHistoryArchives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionHistoryArchive = await _context.TransactionHistoryArchives.FindAsync(id);
            if (transactionHistoryArchive != null)
            {
                _context.TransactionHistoryArchives.Remove(transactionHistoryArchive);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionHistoryArchiveExists(int id)
        {
            return _context.TransactionHistoryArchives.Any(e => e.TransactionId == id);
        }
    }
}
