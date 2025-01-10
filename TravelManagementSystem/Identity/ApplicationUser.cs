using Microsoft.AspNetCore.Identity;

namespace TravelManagementSystem.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}