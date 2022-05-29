using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PVK.Meetups.Web.Data
{
    public class PVKMeetupsDbContext : IdentityDbContext<IdentityUser>
    {
        public PVKMeetupsDbContext(DbContextOptions<PVKMeetupsDbContext> options) : base(options)
        {

        }
    }
}
