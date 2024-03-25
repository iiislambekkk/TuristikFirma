using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuristikFirma.TuristikFirma.Core.Models;
using TuristikFirma.TuristikFirma.DataAccess.Entities;

namespace TuristikFirma.TuristikFirma.DataAccess.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Title).HasMaxLength(Post.MAX_TITLE_LENGTH);

            builder.Property(b => b.Description).IsRequired();

            builder.Property(b => b.Price).IsRequired();
        }
    }
}
