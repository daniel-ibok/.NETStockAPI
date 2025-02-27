using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new();
    }
}