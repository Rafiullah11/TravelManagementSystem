using TravelManagementSystem.Models;

namespace TravelManagementSystem.ViewModel
{
    public class SalesHeaderViewModel
    {
        public int Id { get; set; }
        public string AgentName { get; set; }
        public string PhoneNo { get; set; }
        public string OfficeAddress { get; set; }
        public List<SalesTable> SalesLines { get; set; }
    }
}
