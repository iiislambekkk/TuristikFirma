using TuristikFirma.Abstractions;
using TuristikFirma.Models;
using TuristikFirma.TuristikFirma.DataAccess.Repositories;

namespace TuristikFirma.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<List<Comment>> GetAllComments(Guid entityId)
        {
            return await _commentsRepository.GetAllByEntityId(entityId);
        }


        public async Task<Guid> CreateComment(Comment comment)
        {
            return await _commentsRepository.Create(comment);
        }

        public async Task<Guid> UpdateComment(Guid id, string text, string date, Guid entityId, Guid userId, Guid parentId)
        {
            return await _commentsRepository.Update(id, text, date, entityId, userId, parentId);
        }

        public async Task DeleteCommentAndChildren(Guid id)
        {
            var childrenComments = await _commentsRepository.GetAllDaughters(id);

            Console.Write(childrenComments);

            if (childrenComments == null)
            {
                await _commentsRepository.Delete(id);
                return;
            }

            foreach (var childComment in childrenComments)
            {
                await DeleteCommentAndChildren(childComment.Id);
                await _commentsRepository.Delete(id);
            }

            await _commentsRepository.Delete(id);
        }

        public async Task DeleteAllCommentsFromEntity(Guid entityId)
        {
            var childrenComments = await _commentsRepository.GetAllByEntityId(entityId);

            if (childrenComments == null)
            {
                return;
            }

            foreach (var childComment in childrenComments)
            {
                await _commentsRepository.Delete(childComment.Id);
            }
        }
    }
}
