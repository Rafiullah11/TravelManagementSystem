using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TravelManagementSystem.Models;

namespace TravelManagementSystem.ViewModel
{
    public class CustomerCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        public string PassportNo { get; set; }
        public string Address { get; set; }

        [Display(Name = "Country")]
        public CountryEnum Country { get; set; }

        [Display(Name = "Scan Document")]
        public IFormFile ScanDocument { get; set; }
    }
}
