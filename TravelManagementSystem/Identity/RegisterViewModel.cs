using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.Identity
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]

        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember Me")]
        public string RememberMe { get; set; }
    }
}
