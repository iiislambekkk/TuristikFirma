using TuristikFirma.Models;

namespace TuristikFirma.Abstractions
{
    public interface IToursService
    {
        Task<Guid> CreateTour(Tour tour);
        Task<Guid> DeleteTour(Guid id);
        Task<List<Tour>> GetAllTours();
        Task<Tour> GetOneTour(Guid id);
        Task<Guid> UpdateTour(Guid id, string titleEn, string titleKz, string titleRu, string descriptionEn, string descriptionKz, string descriptionRu, decimal price, string previewPhotoPath, string country);
    }
}