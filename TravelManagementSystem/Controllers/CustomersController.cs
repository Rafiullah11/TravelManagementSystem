using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelManagementSystem.Data;
using TravelManagementSystem.Models;
using TravelManagementSystem.ViewModel;

namespace TravelManagementSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, ILogger<CustomersController> logger)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
           
            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View(new CustomerCreateViewModel());
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string scanDocumentPath = null;

                if (model.ScanDocument != null && model.ScanDocument.Length > 0)
                {
                    scanDocumentPath = UploadFile(model.ScanDocument);
                }

                var customer = new Customer
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    PassportNo = model.PassportNo,
                    Address = model.Address,
                    Country = model.Country,
                    ScanDocumentPath = scanDocumentPath
                };

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            var editViewModel = new CustomerEditViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                PassportNo = customer.PassportNo,
                Address = customer.Address,
                Country = customer.Country,
                ExistingScanDocumentPath = customer.ScanDocumentPath
            };

            return View(editViewModel);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    return NotFound();
                }

                customer.Name = model.Name;
                customer.Phone = model.Phone;
                customer.PassportNo = model.PassportNo;
                customer.Address = model.Address;
                customer.Country = model.Country;

                if (model.ScanDocumentFile != null)
                {
                    if (!string.IsNullOrEmpty(customer.ScanDocumentPath))
                    {
                        string oldFilePath = Path.Combine(_hostEnvironment.WebRootPath, "documents", customer.ScanDocumentPath);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    customer.ScanDocumentPath = UploadFile(model.ScanDocumentFile);
                }

                _context.Update(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            var detailsViewModel = new CustomerDetailsViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                PassportNo = customer.PassportNo,
                Address = customer.Address,
                Country = customer.Country,
                ExistingScanDocumentPath = customer.ScanDocumentPath
            };

            return View(detailsViewModel);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(customer.ScanDocumentPath))
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, "documents", customer.ScanDocumentPath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string UploadFile(IFormFile documentFile)
        {
            string uniqueFileName = null;

            if (documentFile != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "documents");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + documentFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    documentFile.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
