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
            var tourEntities = await _context.Tours
                .AsNoTracking()
                .ToListAsync();

            var tours = tourEntities
                .Select(b => Tour.Create(b.Id, b.TitleEn, b.TitleKz, b.TitleRu, b.DescriptionEn, b.DescriptionKz, b.DescriptionRu, b.Price, b.PreviewPhotoPath, b.Country, b.DaysEn, b.DaysKz, b.DaysRu, b.NumOfDays).Tour)
                .ToList();

            return tours;
        }


        public async Task<Tour> GetOne(Guid id)
        {
            var tourEntity = _context.Tours.Find(id);

            if (tourEntity == null)
            {
                return Tour.Create(id,
                "Error", "Error", "Error",
                "Error", "Error", "Error",
                0, "Error", "Error", "Error", "Error", "Error", 0).Tour;
            }

            var tour = Tour.Create(tourEntity.Id,
                tourEntity.TitleEn, tourEntity.TitleKz, tourEntity.TitleRu,
                tourEntity.DescriptionEn, tourEntity.DescriptionKz, tourEntity.DescriptionRu,
                tourEntity.Price, tourEntity.PreviewPhotoPath, tourEntity.Country, tourEntity.DaysEn, tourEntity.DaysKz, tourEntity.DaysRu, tourEntity.NumOfDays).Tour;

            return tour;
        }

        public async Task<Guid> Create(Tour tour)
        {
            var tourEntity = new TourEntity
            {
                Id = tour.Id,

                TitleEn = tour.TitleEn,
                TitleKz = tour.TitleKz,
                TitleRu = tour.TitleRu,

                DescriptionEn = tour.DescriptionEn,
                DescriptionKz = tour.DescriptionKz,
                DescriptionRu = tour.DescriptionRu,

                PreviewPhotoPath = tour.PreviewPhotoPath,
                Country = tour.Country,

                Price = tour.Price,

                DaysEn = tour.DaysEn,
                DaysKz = tour.DaysKz,
                DaysRu = tour.DaysRu,

                NumOfDays = tour.NumOfDays,
            };

            await _context.Tours.AddAsync(tourEntity);
            await _context.SaveChangesAsync();

            return tourEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string titleEn, string titleKz, string titleRu, string descriptionEn, string descriptionKz, string descriptionRu, decimal price, string previewPhotoPath, string country, string daysEn, string daysKz, string daysRu, int numOfDays)
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
                    .SetProperty(b => b.DaysEn, b => daysEn)
                    .SetProperty(b => b.DaysKz, b => daysKz)
                    .SetProperty(b => b.DaysRu, b => daysRu)
                    .SetProperty(b => b.NumOfDays, b => numOfDays)
                );

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Tours
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            _context.SaveChanges();

            return id;
        }
    }
}
