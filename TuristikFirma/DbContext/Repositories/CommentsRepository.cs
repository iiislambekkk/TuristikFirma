using TuristikFirma.Models;
using TuristikFirma.TuristikFirma.DataAccess.Entities;
using TuristikFirma.TuristikFirma.DataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TuristikFirma.DbContext.Entities;
using TuristikFirma.Abstractions;

namespace TuristikFirma.DbContext.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly TuristikFirmaDbContext _context;

        public CommentsRepository(TuristikFirmaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllByEntityId(Guid entityId)
        {
            var commentEntities = await _context.Comments
            .Where(c => c.EntityId == entityId)
            .AsNoTracking()
            .ToListAsync();

            var comments = commentEntities
                .Select(c => Comment.Create(c.Id, c.Text, c.Date, c.EntityId, c.UserId, c.ParentId).Comment)
                .ToList();

            return comments;
        }


        public async Task<List<Comment>> GetAllDaughters(Guid id)
        {
            var commentEntities = await _context.Comments
            .Where(c => c.ParentId == id)
            .AsNoTracking()
            .ToListAsync();

            var comments = commentEntities
                .Select(c => Comment.Create(c.Id, c.Text, c.Date, c.EntityId, c.UserId, c.ParentId).Comment)
                .ToList();

            return comments;
        }

        public async Task<Guid> Create(Comment comment)
        {
            var commentEntity = new CommentEntity
            {
                Id = comment.Id,
                Text = comment.Text,
                Date = comment.Date,
                UserId = comment.UserId,
                EntityId = comment.EntityId,
                ParentId = comment.ParentId,
            };

            await _context.Comments.AddAsync(commentEntity);
            await _context.SaveChangesAsync();

            return commentEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string text, string date, Guid entityId, Guid userId, Guid parentId)
        {
            await _context.Comments
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.Text, c => text)
                    .SetProperty(c => c.Date, c => date)
                    .SetProperty(c => c.EntityId, c => entityId)
                    .SetProperty(c => c.UserId, c => userId)
                    .SetProperty(c => c.ParentId, c => parentId)
                );

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Comments
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            _context.SaveChanges();

            return id;
        }
    }
}
