namespace TravelManagementSystem.Models
{
    public class Agent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WhatsApp { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? OfficeAddress { get; set; }
        // Navigation property
        public ICollection<SalesTable> SalesTables { get; set; }
    }
}
