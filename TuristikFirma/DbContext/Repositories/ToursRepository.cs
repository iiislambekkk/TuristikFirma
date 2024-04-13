using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TuristikFirma.Abstractions;
using TuristikFirma.Models;
using TuristikFirma.TuristikFirma.DataAccess.Entities;

namespace TuristikFirma.TuristikFirma.DataAccess.Repositories
{
    public class ToursRepository : IToursRepository
    {
        private readonly TuristikFirmaDbContext _context;

        public ToursRepository(TuristikFirmaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tour>> GetAll()
        {
            var postEntities = await _context.Tours
                .AsNoTracking()
                .ToListAsync();

            var posts = postEntities
                .Select(b => Tour.Create(b.Id, b.TitleEn, b.TitleKz, b.TitleRu, b.DescriptionEn, b.DescriptionKz, b.DescriptionRu, b.Price, b.PreviewPhotoPath, b.Country).Post)
                .ToList();

            return posts;
        }

        
    public async Task<Tour> GetOne(Guid id)
        {
            var tourEntity =  _context.Tours.Find(id);
            
            if (tourEntity == null)
            {
                return Tour.Create(id,
                "Error", "Error", "Error",
                "Error", "Error", "Error",
                0, "Error", "Error").Post;
            }

            var tour = Tour.Create(tourEntity.Id, 
                tourEntity.TitleEn, tourEntity.TitleKz, tourEntity.TitleRu, 
                tourEntity.DescriptionEn, tourEntity.DescriptionKz, tourEntity.DescriptionRu, 
                tourEntity.Price, tourEntity.PreviewPhotoPath, tourEntity.Country).Post;

            return tour;
        }

        public async Task<Guid> Create(Tour post)
        {
            var postEntity = new TourEntity
            {
                Id = post.Id,

                TitleEn = post.TitleEn,
                TitleKz = post.TitleKz,
                TitleRu = post.TitleRu,

                DescriptionEn = post.DescriptionEn,
                DescriptionKz = post.DescriptionKz,
                DescriptionRu = post.DescriptionRu,

                PreviewPhotoPath = post.PreviewPhotoPath,
                Country = post.Country,

                Price = post.Price
            };

            await _context.Tours.AddAsync(postEntity);
            await _context.SaveChangesAsync();

            return postEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string titleEn, string titleKz, string titleRu, string descriptionEn, string descriptionKz, string descriptionRu, decimal price, string previewPhotoPath, string country)
        {
            await _context.Tours
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.TitleEn, b => titleEn)
                    .SetProperty(b => b.TitleKz, b => titleKz)
                    .SetProperty(b => b.TitleRu, b => titleRu)
                    .SetProperty(b => b.DescriptionEn, b => descriptionEn)
                    .SetProperty(b => b.DescriptionKz, b => descriptionKz)
                    .SetProperty(b => b.DescriptionRu, b => descriptionRu)
                    .SetProperty(b => b.PreviewPhotoPath, b => previewPhotoPath)
                    .SetProperty(b => b.Country, b => country)
                    .SetProperty(b => b.Price, b => price)
                );

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Tours
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
