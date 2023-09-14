using Microsoft.AspNetCore.Identity;

namespace SA_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
