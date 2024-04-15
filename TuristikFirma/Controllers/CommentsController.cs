using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuristikFirma.Abstractions;
using TuristikFirma.Contracts;
using TuristikFirma.Models;


//00000000-0000-0000-0000-000000000000
namespace TuristikFirma.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<ToursResponse>>> GetTour(Guid id)
        {
            var comments = await _commentsService.GetAllComments(id);


            var response = comments.Select(c => new CommentsResponse(c.Id, c.Text, c.Date, c.EntityId, c.UserId, c.ParentId));

            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<Guid>> CreateComment([FromBody] CommentsRequest request)
        {
            var (comment, error) = Comment.Create(
                Guid.NewGuid(),
                request.Text,
                request.Date,
                request.EntityId,
                request.UserId,
                request.ParentId
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var commentId = await _commentsService.CreateComment(comment);

            return Ok(commentId);
        }

        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<Guid>> UpdateComment(Guid id, [FromBody] CommentsRequest request)
        {
            var commentId = await _commentsService.UpdateComment(
                id, 
                request.Text,
                request.Date,
                request.EntityId,
                request.UserId,
                request.ParentId
                );

            return Ok(commentId);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize("OnlyAdmin")]
        public async Task<ActionResult> DeleteCommentAndChildren(Guid id)
        {
            await _commentsService.DeleteCommentAndChildren(id);
            return Ok("Gut");
        }

        [HttpPut("deleteOne/{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> DeleteOnlyOneComment(Guid id, [FromBody] CommentsRequest request)
        {
            var commentId = await _commentsService.UpdateComment(
                id,
                "Комментарии удалён",
                request.Date,
                request.EntityId,
                request.UserId,
                request.ParentId
                );

            return Ok(commentId);
        }
    }
}
