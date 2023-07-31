using Microsoft.AspNetCore.Identity;

namespace E_PROJECT_MANAGER.Models
{
    public class CustomUser : IdentityUser
    {
        public string FullName { get; set; } = "";
        public int Age { get; set; } = 20;
    }
}
