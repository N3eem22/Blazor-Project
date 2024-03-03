using Microsoft.AspNetCore.Identity;

namespace Test5Blazor.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FName { get; set; }
        public string? LName { get; set; }
    }
}
