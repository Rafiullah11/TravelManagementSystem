using System.ComponentModel.DataAnnotations;

namespace TravelManagementSystem.Identity
{
    public class RoleEditViewModel
    {
        public RoleEditViewModel()
        {
            User = new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name is requierd")]
        public string RoleName { get; set; }

        public List<string> User { get; set; }
    }
}
