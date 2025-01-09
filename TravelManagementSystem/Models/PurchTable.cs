namespace TravelManagementSystem.Models
{
    public class PurchTable
    {
            public int Id { get; set; }

            // Navigation properties for relationships
            public Agent? Agent { get; set; }
            public Customer? Customer { get; set; }

            public int AgentId { get; set; }
            public int CustomerId { get; set; }

            // Other properties
            public CountryEnum Country { get; set; }
            public string? Company { get; set; }
            public Trade Trade { get; set; }
            public string? SubTrade { get; set; }
            public DateTime FlightOn { get; set; }
            public string? Destination { get; set; }
            //public string? Country { get; set; }
            public decimal Credit { get; set; }
            public decimal Debit { get; set; }
            public decimal? Balance { get; set; }
            public int? CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; } = DateTime.Now;
        
    }
}
