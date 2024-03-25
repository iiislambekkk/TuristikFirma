using TuristikFirma.Models;

namespace TuristikFirma.Abstractions
{
    public interface IPostsRepository
    {
        Task<Guid> Create(Post post);
        Task<Guid> Delete(Guid id);
        Task<List<Post>> Get();
        Task<Guid> Update(Guid id, string title, string description, decimal price);
    }
}