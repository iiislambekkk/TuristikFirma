using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TuristikFirma.TuristikFirma.DataAccess.Entities;
using TuristikFirma.Models;
using TuristikFirma.TuristikFirma.DataAccess.Configurations;
using TuristikFirma.DbContext.Configurations;
using TuristikFirma.DbContext.Entities;

namespace TuristikFirma.TuristikFirma.DataAccess 
{
    public class TuristikFirmaDbContext : IdentityDbContext<User>
    {
        public TuristikFirmaDbContext(DbContextOptions<TuristikFirmaDbContext> options) : base(options)
        {
            
        }

        public DbSet<TourEntity> Tours { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TourConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
        }
    }
}
