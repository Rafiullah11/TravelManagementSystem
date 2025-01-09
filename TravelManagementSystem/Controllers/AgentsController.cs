using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystem.Data;
using TravelManagementSystem.Models;
using TravelManagementSystem.ViewModel;
using TravelManagementSystem.Helpers;

namespace TravelManagementSystem.Controllers
{
    public class AgentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> HeaderAndLine(int Id)
        {
            //int agentId = 1;
            // Fetch the agent's header information
            var agent = await _context.Agents
                .FirstOrDefaultAsync(a => a.Id == Id);

            if (agent == null)
            {
                return NotFound("Agent not found.");
            }

            // Fetch related sales lines for the agent
            var purchLines = await _context.PurchTables
                .Where(s => s.AgentId == Id)
                .Include(s => s.Customer) // Include related Customer data if needed
                .ToListAsync();

            // Map data to the SalesHeaderViewModel
            var viewModel = new PurchHeaderViewModel
            {
                Id=agent.Id,
                AgentName = agent.Name,
                PhoneNo = agent.Phone,
                OfficeAddress = agent.OfficeAddress,
                PurchaseTables = purchLines,
                //SalesLines = salesLines
            };

            return View(viewModel);
        }

        public async Task<IActionResult> CreateLine(int id)
        {
            // Fetch the agent's information
            var agent = await _context.Agents
                .FirstOrDefaultAsync(a => a.Id == id);

            if (agent == null)
            {
                return NotFound("Agent not found.");
            }

            // Fetch customers for the dropdown
            var customers = await _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

            // Create a ViewModel to pass to the view
            var viewModel = new PurchLineCreateViewModel
            {
                AgentId = agent.Id,
                AgentName = agent.Name,
                PhoneNo = agent.Phone,
                OfficeAddress = agent.OfficeAddress,
                Customers = customers
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLine(PurchLineCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new SalesTable entity
                var salesLine = new SalesTable
                {
                    AgentId = model.AgentId,
                    CustomerId = (int)model.CustomerId, // Set the selected CustomerId
                    Company = model.Company,
                    Trade = model.Trade,
                    SubTrade = model.SubTrade,
                    FlightOn = model.FlightOn,
                    Destination = model.Destination,
                    Country = model.Country,
                    Credit = model.Credit,
                    Debit = model.Debit,
                    CreatedOn = DateTime.Now
                };

                // Save to database
                _context.SalesTables.Add(salesLine);
                await _context.SaveChangesAsync();

                // Redirect back to the header and line page
                return RedirectToAction("HeaderAndLine", new { id = model.AgentId });
            }

            // If validation fails, return the view with the model
            return View(model);
        }


        // GET: Agents
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

            // Build the query
            var agents = from a in _context.Agents
                         select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                agents = agents.Where(s => s.Name.Contains(searchString));
            }

            // Define the page size
            int pageSize = 3;

            // Return the paginated list
            return View(await PaginatedList<Agent>.CreateAsync(agents.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Agents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,WhatsApp,Phone,Email,OfficeAddress")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            return View(agent);
        }

        // POST: Agents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,WhatsApp,Phone,Email,OfficeAddress")] Agent agent)
        {
            if (id != agent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.Id))
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
            return View(agent);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agent = await _context.Agents.FindAsync(id);
            if (agent != null)
            {
                _context.Agents.Remove(agent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.Id == id);
        }
    }
}
