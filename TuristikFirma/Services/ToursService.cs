using TuristikFirma.Abstractions;
using TuristikFirma.Models;

namespace TuristikFirma.Services
{
    public class ToursService : IToursService
    {
        private readonly IToursRepository _toursRepository;

        public ToursService(IToursRepository toursRepository)
        {
            _toursRepository = toursRepository;
        }

        public async Task<List<Tour>> GetAllTours()
        {
            return await _toursRepository.GetAll();
        }

        public async Task<Tour> GetOneTour(Guid id)
        {
            return await _toursRepository.GetOne(id);
        }

        public async Task<Guid> CreateTour(Tour tour)
        {
            return await _toursRepository.Create(tour);
        }

        public async Task<Guid> UpdateTour(Guid id, string titleEn, string titleKz, string titleRu, string descriptionEn, string descriptionKz, string descriptionRu, decimal price, string previewPhotoPath, string country, string daysEn, string daysKz, string daysRu, int numOfDays)
        {
            return await _toursRepository.Update(id, titleEn, titleKz, titleRu, descriptionEn, descriptionKz, descriptionRu, price, previewPhotoPath, country, daysEn, daysKz, daysRu, numOfDays);
        }
        public async Task<Guid> DeleteTour(Guid id)
        {
            return await _toursRepository.Delete(id);
        }
    }
}
