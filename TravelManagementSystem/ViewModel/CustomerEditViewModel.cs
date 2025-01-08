using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TravelManagementSystem.Models;

namespace TravelManagementSystem.ViewModel
{
    public class CustomerEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string PassportNo { get; set; }

        public string Address { get; set; }

        [Required]
        [Display(Name = "Country")]
        public CountryEnum Country { get; set; }

        [Display(Name = "Scan Document")]
        public IFormFile ScanDocumentFile { get; set; }

        public string? ExistingScanDocumentPath { get; set; }
    }
}

