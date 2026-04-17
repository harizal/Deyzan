using Flagging.Taspen.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flagging.Taspen.Web.DBContext
{
    public class AppDataContext(DbContextOptions<AppDataContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Peserta> Peserta { get; set; }
    }
}
