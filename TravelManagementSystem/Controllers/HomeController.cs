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
            var customerCount = await _context.Customers.CountAsync(); // Get customer count
            ViewBag.CustomerCount = customerCount;                    // Pass count to the view

            var agentCount = await _context.Agents.CountAsync();
            ViewBag.AgentCount = agentCount;

            var totalSaleCount = await _context.SalesTables.CountAsync();
            ViewBag.TotalSaleCount = totalSaleCount;
            return View();
        }

        public IActionResult Privacy()
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
