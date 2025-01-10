using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TravelManagementSystem.Data;
using TravelManagementSystem.Models;

namespace TravelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Customer Count
            var customerCount = await _context.Customers.CountAsync();
            ViewBag.CustomerCount = customerCount;

            // Agent Count
            var agentCount = await _context.Agents.CountAsync();
            ViewBag.AgentCount = agentCount;

            // Total Sale Count
            var totalSaleCount = await _context.SalesTables.CountAsync();
            ViewBag.TotalSaleCount = totalSaleCount;

            // Monthly Sales Data
            var salesData = await _context.SalesTables
            .Where(s => s.CreatedOn.HasValue) // Filter out null dates
            .GroupBy(s => s.CreatedOn.Value.Month) // Group by the Month number
            .Select(g => new
            {
                Month = g.Key, // This should be an integer representing the month
                TotalSales = g.Count() // Or sum if required
            })
            .ToListAsync();

            // Convert the data into a dictionary with month names
            var salesDictionary = new Dictionary<string, int>();
            foreach (var item in salesData)
            {
                string monthName = new DateTime(1, item.Month, 1).ToString("MMMM"); // Convert month number to name
                salesDictionary[monthName] = item.TotalSales;
            }

            ViewBag.SalesLabels = salesDictionary.Keys.ToArray();  // Pass month names to the view
            ViewBag.SalesValues = salesDictionary.Values.ToArray(); // Pass sales data to the view

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult DashboardSalesChart()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
