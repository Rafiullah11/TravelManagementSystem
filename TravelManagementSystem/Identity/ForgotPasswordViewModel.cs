using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.Identity
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
