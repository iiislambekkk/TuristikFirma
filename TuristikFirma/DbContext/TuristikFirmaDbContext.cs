using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TuristikFirma.TuristikFirma.DataAccess.Entities;
using TuristikFirma.Models;

namespace TuristikFirma.TuristikFirma.DataAccess 
{
    public class TuristikFirmaDbContext : IdentityDbContext<User>
    {
        public TuristikFirmaDbContext(DbContextOptions<TuristikFirmaDbContext> options) : base(options)
        {
            
        }

        public DbSet<TourEntity> Tours { get; set; }
    }
}
