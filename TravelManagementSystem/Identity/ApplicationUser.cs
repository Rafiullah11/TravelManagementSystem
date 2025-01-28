using Microsoft.AspNetCore.Identity;

namespace TravelManagementSystem.Identity
{
    public class ApplicationUser : IdentityUser
    {
		public string? FullName { get; set; }
	}
}