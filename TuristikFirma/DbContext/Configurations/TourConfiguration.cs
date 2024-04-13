using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TuristikFirma.Models;
using TuristikFirma.TuristikFirma.DataAccess.Entities;

namespace TuristikFirma.TuristikFirma.DataAccess.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<TourEntity>
    {
        public void Configure(EntityTypeBuilder<TourEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.TitleEn).HasMaxLength(Tour.MAX_TITLE_LENGTH);
            builder.Property(b => b.TitleKz).HasMaxLength(Tour.MAX_TITLE_LENGTH);
            builder.Property(b => b.TitleRu).HasMaxLength(Tour.MAX_TITLE_LENGTH);

            builder.Property(b => b.DescriptionEn).IsRequired();
            builder.Property(b => b.DescriptionKz).IsRequired();
            builder.Property(b => b.DescriptionRu).IsRequired();
            builder.Property(b => b.PreviewPhotoPath);
            builder.Property(b => b.Country).IsRequired();
            builder.Property(b => b.Country).IsRequired();

            builder.Property(b => b.Price).IsRequired();
        }
    }
}

