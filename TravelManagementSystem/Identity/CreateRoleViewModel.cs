using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.Identity
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
