using Microsoft.AspNetCore.Identity;

namespace Flagging.Taspen.Web.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
