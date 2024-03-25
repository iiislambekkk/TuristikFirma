using TuristikFirma.TuristikFirma.Core.Abstractions;
using TuristikFirma.TuristikFirma.Core.Models;

namespace TuristikFirma.Turistik.Firma.Application.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;

        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _postsRepository.Get();
        }

        public async Task<Guid> CreatePost(Post book)
        {
            return await _postsRepository.Create(book);
        }

        public async Task<Guid> UpdatePost(Guid id, string title, string description, decimal price)
        {
            return await _postsRepository.Update(id, title, description, price);
        }

        public async Task<Guid> DeletePost(Guid id)
        {
            return await _postsRepository.Delete(id);
        }
    }
}
