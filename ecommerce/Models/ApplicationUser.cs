using Microsoft.AspNetCore.Identity;

namespace ecommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
