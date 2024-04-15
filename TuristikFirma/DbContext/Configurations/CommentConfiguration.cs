using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TuristikFirma.Models;
using TuristikFirma.DbContext.Entities;

namespace TuristikFirma.DbContext.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Date).IsRequired();
            builder.Property(c => c.EntityId).IsRequired();
            builder.Property(c => c.ParentId);
            builder.Property(c => c.UserId).IsRequired();
        }
    }
}
