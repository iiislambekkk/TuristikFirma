using TuristikFirma.Models;

namespace TuristikFirma.TuristikFirma.DataAccess.Repositories
{
    public interface IToursRepository
    {
        Task<Guid> Create(Tour post);
        Task<Guid> Delete(Guid id);
        Task<Tour> GetOne(Guid id);
        Task<List<Tour>> GetAll();
        Task<Guid> Update(Guid id, string titleEn, string titleKz, string titleRu, string descriptionEn, string descriptionKz, string descriptionRu, decimal price, string previewPhotoPath, string country);
    }
}