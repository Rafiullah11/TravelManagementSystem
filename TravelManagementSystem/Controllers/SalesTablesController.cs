using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystem.Data;
using TravelManagementSystem.Helpers;
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

        //// GET: SalesTables
        //public async Task<IActionResult> Index()
        //{
        //    var salesTables = await _context.SalesTables
        //        .Include(s => s.Agent)
        //        .Include(s => s.Customer)
        //        .ToListAsync();
        //    return View(salesTables);
        //}
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

            var salesTables =  _context.SalesTables
               .Include(s => s.Agent)
               .Include(s => s.Customer)
               .AsQueryable();

            // Apply filtering if a search string is provided
            if (!string.IsNullOrEmpty(searchString))
            {
                salesTables = salesTables.Where(s => s.Agent.Name.Contains(searchString) || s.Customer.Name.Contains(searchString));
            }

            // Define the page size
            int pageSize = 5;

            // Return the paginated list
            return View(await PaginatedList<SalesTable>.CreateAsync(salesTables.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        public async Task<IActionResult> Create( SalesTable salesTable)
        {
            if (ModelState.IsValid)
            {
                // Calculate balance
                salesTable.Balance = salesTable.Debit - salesTable.Credit;
                salesTable.CreatedOn = DateTime.Now;

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

			// Query to fetch SalesTable with related Agent and Customer details
			var salesTable = await _context.SalesTables
				.Where(s => s.Id == id)
				.Include(s => s.Agent) // Include Agent details
				.Include(s => s.Customer) // Include Customer details
				.FirstOrDefaultAsync();

			if (salesTable == null)
				return NotFound();

			// Populate ViewData for dropdowns
			ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", salesTable.AgentId);
			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", salesTable.CustomerId);

			return View(salesTable);
		}

		// POST: SalesTables/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, SalesTable salesTable)
		{
			if (id != salesTable.Id)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
                    // Fetch the previous balance (C2) from the database
                    var previousBalance = _context.SalesTables
                        .Where(s => s.CustomerId == salesTable.CustomerId)
                        .OrderByDescending(s => s.CreatedOn)
                        .Select(s => s.Balance)
                        .FirstOrDefault(); // Get the last recorded balance or default to 0

                    // Apply the formula: IF(AND(Credit == 0, Debit == 0), 0, (Credit - Debit) + PreviousBalance)
                    var balance = (salesTable.Credit == 0 && salesTable.Debit == 0) ? 0 : (salesTable.Debit - salesTable.Credit) + previousBalance;

                    // Update the balance
                    salesTable.Balance = salesTable.Debit - salesTable.Credit;

					// Update the SalesTable entity
					_context.Update(salesTable);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SalesTableExists(salesTable.Id))
						return NotFound();
					throw;
				}

				// Redirect back to Index after successful update
				return RedirectToAction(nameof(Index));
			}

			// Repopulate ViewData in case of validation errors
			ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", salesTable.AgentId);
			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", salesTable.CustomerId);

			return View(salesTable);
		}


        //SalesLines Update method

        // GET: SalesTables/SalesLinesUpdate/5
        public async Task<IActionResult> SalesLinesUpdate(int? id)
        {
            if (id == null)
                return NotFound();

            // Query to fetch SalesTable with related Agent and Customer details
            var salesTable = await _context.SalesTables
                .Where(s => s.Id == id)
                .Include(s => s.Agent) // Include Agent details
                .Include(s => s.Customer) // Include Customer details
                .FirstOrDefaultAsync();

            if (salesTable == null)
                return NotFound();

            // Populate ViewData for dropdowns
            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", salesTable.AgentId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", salesTable.CustomerId);

            return View(salesTable);
        }
        // POST: SalesTables/SalesLinesUpdate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SalesTables/SalesLinesUpdate/{id}")]
        public async Task<IActionResult> SalesLinesUpdate(int id, SalesTable salesTable, int customerId)
        {
            if (id != salesTable.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the previous balance (C2) from the database
                    var previousBalance = _context.SalesTables
                        .Where(s => s.CustomerId == salesTable.CustomerId)
                        .OrderByDescending(s => s.CreatedOn)
                        .Select(s => s.Balance)
                        .FirstOrDefault(); // Get the last recorded balance or default to 0

                    // Apply the formula: IF(AND(Credit == 0, Debit == 0), 0, (Credit - Debit) + PreviousBalance)
                    var balance = (salesTable.Credit == 0 && salesTable.Debit == 0) ? 0 : (salesTable.Debit - salesTable.Credit) + previousBalance;

                    // Update the balance
                    salesTable.Balance = salesTable.Debit - salesTable.Credit;

                    // Update the SalesTable entity
                    _context.Update(salesTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesTableExists(salesTable.Id))
                        return NotFound();
                    throw;
                }

                // Redirect back to Index after successful update
                return RedirectToAction("HeaderAndLine", "Customers", new { id = customerId });

            }

            // Repopulate ViewData in case of validation errors
            ViewData["AgentId"] = new SelectList(_context.Agents, "Id", "Name", salesTable.AgentId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", salesTable.CustomerId);

            return RedirectToAction("HeaderAndLine", "Customers", new { id = customerId });

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, SalesTable model)
        {
            var salesTable = await _context.SalesTables.FindAsync(id);
            _context.SalesTables.Remove(salesTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        // GET: SalesTables/Delete/5
		public async Task<IActionResult> DeleteSalesLine(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SalesTables/DeleteSalesLine/{id}")]
        public async Task<IActionResult> DeleteSalesLine(int id, int customerId)
        {
            var salesTable = await _context.SalesTables.FindAsync(id);
            if (salesTable != null)
            {
                _context.SalesTables.Remove(salesTable);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("HeaderAndLine", "Customers", new { id = customerId });
        }

        private bool SalesTableExists(int id)
        {
            return _context.SalesTables.Any(e => e.Id == id);
        }
    }
}
