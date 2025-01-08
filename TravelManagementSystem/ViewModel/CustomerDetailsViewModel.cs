using TravelManagementSystem.Models;

namespace TravelManagementSystem.ViewModel
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PassportNo { get; set; }
        public string Address { get; set; }
        public CountryEnum Country { get; set; }
        public string ExistingScanDocumentPath { get; set; }
    }
}