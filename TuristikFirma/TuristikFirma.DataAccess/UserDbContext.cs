using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TuristikFirma.TuristikFirma.DataAccess.Entities;

namespace TuristikFirma.TuristikFirma.DataAccess
{
    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<PostEntity> Posts { get; set; }
    }
}
