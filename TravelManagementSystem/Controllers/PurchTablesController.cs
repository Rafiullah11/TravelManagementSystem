using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystem.Data;
using TravelManagementSystem.Helpers;
using TravelManagementSystem.Models;

namespace TravelManagementSystem.Controllers
{
    public class PurchTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchTables
        //public async Task<IActionResult> Index()
        //{
        //    var purchTables = await _context.PurchTables
        //        .Include(s => s.Agent)
        //        .Include(s => s.Customer)
        //        .ToListAsync();
        //    return View(purchTables);
        //}
        // GET: PurchTables
        public async Task<IActionResult> Index(int? pageNumber, string currentFilter, string searchString)
        {
            // If a new search string is provided, reset to the first page
            if (!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Pass the current filter to the view
            ViewData["CurrentFilter"] = searchString;

            // Build the query for PurchTables
            var purchTables = _context.PurchTables
                .Include(s => s.Agent)
                .Include(s => s.Customer)
                .AsQueryable();

            // Apply filtering if a search string is provided
            if (!string.IsNullOrEmpty(searchString))
            {
                purchTables = purchTables.Where(s => s.Customer.Name.Contains(searchString) || s.Agent.Name.Contains(searchString));
            }

            // Define the page size
            int pageSize = 5;

            // Return the paginated list
            return View(await PaginatedList<PurchTable>.CreateAsync(purchTables.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: PurchTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var purchTables = await _context.PurchTables
                 .Include(s => s.Agent)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (purchTables == null)
                return NotFound();

            return View(purchTables);
        }

        // GET: PurchTables/Create
        public IActionResult Create()
        {
            // Ensure _context.Agents and _context.Customers are not null
            var agents = _context.Agents?.ToList() ?? new List<Agent>();
            var customers = _context.Customers?.ToList() ?? new List<Customer>();

            ViewBag.AgentId = new SelectList(agents, "Id", "Name");
            ViewBag.CustomerId = new SelectList(customers, "Id", "Name");

            return View();
        }


        // POST: PurchTables/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Company,Trade,SubTrade,FlightOn,Destination,Country,Credit,Debit,AgentId,CustomerId,CreatedBy")] PurchTable PurchTable)
        {
            if (ModelState.IsValid)
            {
                // Calculate balance
                PurchTable.Balance = PurchTable.Credit - PurchTable.Debit;
                PurchTable.CreatedOn = DateTime.Now;

                _context.Add(PurchTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", PurchTable.AgentId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", PurchTable.CustomerId);
            return View(PurchTable);
        }

        // GET: PurchTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var purchTable = await _context.PurchTables.FindAsync(id);
            if (purchTable == null)
                return NotFound();

            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", purchTable.AgentId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", purchTable.CustomerId);
            return View(purchTable);
        }

        // POST: PurchTables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Company,Trade,SubTrade,FlightOn,Destination,Country,Credit,Debit,Balance,AgentId,CustomerId,CreatedBy,CreatedAt")] PurchTable purchTable)
        {
            if (id != purchTable.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Recalculate balance on edit
                    purchTable.Balance = purchTable.Credit - purchTable.Debit;

                    _context.Update(purchTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchTableeExists(purchTable.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", purchTable.AgentId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", purchTable.CustomerId);
            return View(purchTable);
        }

        // GET: PurchTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var purchTable = await _context.PurchTables
                .Include(s => s.Agent)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (purchTable == null)
                return NotFound();

            return View(purchTable);
        }

        // POST: PurchTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchTable = await _context.PurchTables.FindAsync(id);
            _context.PurchTables.Remove(purchTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchTableeExists(int id)
        {
            return _context.PurchTables.Any(e => e.Id == id);
        }
    }
}

