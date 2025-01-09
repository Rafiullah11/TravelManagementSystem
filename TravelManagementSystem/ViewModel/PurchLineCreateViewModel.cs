using Microsoft.AspNetCore.Mvc.Rendering;
using TravelManagementSystem.Models;

namespace TravelManagementSystem.ViewModel
{
    public class PurchLineCreateViewModel
    {
        public int AgentId { get; set; }
        public string? AgentName { get; set; }
        public string? PhoneNo { get; set; }
        public string? OfficeAddress { get; set; }

        // Fields for creating a new sales line
        public string? Company { get; set; }
        public Trade Trade { get; set; }
        public string? SubTrade { get; set; }
        public DateTime FlightOn { get; set; }
        public string? Destination { get; set; }
        public CountryEnum Country { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal? Balance { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        // New field for selecting a customer
        public int? CustomerId { get; set; }
        public List<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
    }

}
