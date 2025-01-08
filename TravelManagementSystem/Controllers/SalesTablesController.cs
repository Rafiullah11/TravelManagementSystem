using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystem.Data;
using TravelManagementSystem.Models;

namespace TravelManagementSystem.Controllers
{
    public class SalesTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SalesTables
        public async Task<IActionResult> Index()
        {
            var salesTables = await _context.SalesTables
                .Include(s => s.Agent)
                .Include(s => s.Customer)
                .ToListAsync();
            return View(salesTables);
        }

        // GET: SalesTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var salesTable = await _context.SalesTables
                .Include(s => s.Agent)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salesTable == null)
                return NotFound();

            return View(salesTable);
        }

        // GET: SalesTables/Create
        public IActionResult Create()
        {
            // Ensure _context.Agents and _context.Customers are not null
            var agents = _context.Agents?.ToList() ?? new List<Agent>();
            var customers = _context.Customers?.ToList() ?? new List<Customer>();

            ViewBag.AgentId = new SelectList(agents, "Id", "Name");
            ViewBag.CustomerId = new SelectList(customers, "Id", "Name");

            return View();
        }


        // POST: SalesTables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Company,Trade,SubTrade,FlightOn,Destination,Country,Credit,Debit,AgentId,CustomerId,CreatedBy")] SalesTable salesTable)
        {
            if (ModelState.IsValid)
            {
                // Calculate balance
                salesTable.Balance = salesTable.Credit - salesTable.Debit;
                salesTable.CreatedAt = DateTime.Now;

                _context.Add(salesTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", salesTable.AgentId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", salesTable.CustomerId);
            return View(salesTable);
        }

        // GET: SalesTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var salesTable = await _context.SalesTables.FindAsync(id);
            if (salesTable == null)
                return NotFound();

            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", salesTable.AgentId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", salesTable.CustomerId);
            return View(salesTable);
        }

        // POST: SalesTables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Company,Trade,SubTrade,FlightOn,Destination,Country,Credit,Debit,Balance,AgentId,CustomerId,CreatedBy,CreatedAt")] SalesTable salesTable)
        {
            if (id != salesTable.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Recalculate balance on edit
                    salesTable.Balance = salesTable.Credit - salesTable.Debit;

                    _context.Update(salesTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesTableExists(salesTable.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", salesTable.AgentId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", salesTable.CustomerId);
            return View(salesTable);
        }

        // GET: SalesTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var salesTable = await _context.SalesTables
                .Include(s => s.Agent)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salesTable == null)
                return NotFound();

            return View(salesTable);
        }

        // POST: SalesTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesTable = await _context.SalesTables.FindAsync(id);
            _context.SalesTables.Remove(salesTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesTableExists(int id)
        {
            return _context.SalesTables.Any(e => e.Id == id);
        }
    }
}
