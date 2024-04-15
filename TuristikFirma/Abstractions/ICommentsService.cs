using TuristikFirma.Models;

namespace TuristikFirma.Abstractions
{
    public interface ICommentsService
    {
        Task<Guid> CreateComment(Comment comment);
        Task DeleteAllCommentsFromEntity(Guid entityId);
        Task DeleteCommentAndChildren(Guid id);
        Task<List<Comment>> GetAllComments(Guid entityId);
        Task<Guid> UpdateComment(Guid id, string text, string date, Guid entityId, Guid userId, Guid parentId);
    }
}