using TuristikFirma.Models;

namespace TuristikFirma.Abstractions
{
    public interface ICommentsRepository
    {
        Task<Guid> Create(Comment comment);
        Task<Guid> Delete(Guid id);
        Task<List<Comment>> GetAllByEntityId(Guid entityId);
        Task<List<Comment>> GetAllDaughters(Guid id);
        Task<Guid> Update(Guid id, string text, string date, Guid entityId, Guid userId, Guid parentId);
    }
}