using Microsoft.EntityFrameworkCore;
using TuristikFirma.TuristikFirma.Core.Abstractions;
using TuristikFirma.TuristikFirma.Core.Models;
using TuristikFirma.TuristikFirma.DataAccess.Entities;

namespace TuristikFirma.TuristikFirma.DataAccess.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly TuristikFirmaDbContext _context;

        public PostsRepository(TuristikFirmaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> Get()
        {
            var postEntities = await _context.Posts
                .AsNoTracking()
                .ToListAsync();

            var posts = postEntities
                .Select(b => Post.Create(b.Id, b.Title, b.Description, b.Price).Post)
                .ToList();

            return posts;
        }

        public async Task<Guid> Create(Post post)
        {
            var postEntity = new PostEntity
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Price = post.Price,
            };

            await _context.Posts.AddAsync(postEntity);
            await _context.SaveChangesAsync();

            return postEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _context.Posts
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Title, b => title)
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.Price, b => price)
                );

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Posts
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
