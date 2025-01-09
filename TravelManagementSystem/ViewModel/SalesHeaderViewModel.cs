using TravelManagementSystem.Models;

namespace TravelManagementSystem.ViewModel
{
    public class SalesHeaderViewModel
    {
        public int Id { get; set; }
        public string? CustName { get; set; }
        public string? PhoneNo { get; set; }
        public string? Address { get; set; }
        public List<SalesTable> SalesTable { get; set; }
    }
}
