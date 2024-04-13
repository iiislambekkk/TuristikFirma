using TuristikFirma.Models;

namespace TuristikFirma.Abstractions
{
    public interface IToursRepository
    {
        Task<Guid> Create(Tour tour);
        Task<Guid> Delete(Guid id);
        Task<List<Tour>> GetAll();
        Task<Tour> GetOne(Guid id);
        Task<Guid> Update(Guid id, string titleEn, string titleKz, string titleRu, string descriptionEn, string descriptionKz, string descriptionRu, decimal price, string previewPhotoPath, string country, string daysEn, string daysKz, string daysRu, int numOfDays);
    }
}