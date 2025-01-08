using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TravelManagementSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PassportNo { get; set; }
        public string Address { get; set; }
        public CountryEnum Country { get; set; }
        public string ScanDocumentPath { get; set; }

        [NotMapped]
        public IFormFile ScanDocument { get; set; }

        // Navigation property
        public ICollection<SalesTable> SalesTables { get; set; }
    }
}
