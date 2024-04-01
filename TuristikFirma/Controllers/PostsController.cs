using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TuristikFirma.Abstractions;
using TuristikFirma.Contracts;
using TuristikFirma.Models;

namespace TuristikFirma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService) 
        {
            _postsService = postsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostsResponse>>> GetBooks()
        {
            var books = await _postsService.GetAllPosts();

            var response = books.Select(b => new PostsResponse(b.Id, b.Title, b.Description, b.Price));

            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize("OnlyAdmin")]
        public async Task<ActionResult<Guid>> CreatePost([FromBody] PostsRequest request)
        {
            var (post, error) = Post.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var bookId = await _postsService.CreatePost(post);

            return Ok(bookId);
        }

        [HttpPut("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize("OnlyAdmin")]
        public async Task<ActionResult<Guid>> UpdatePost(Guid id, [FromBody] PostsRequest request)
        {
            var bookId = await _postsService.UpdatePost(id, request.Title, request.Description, request.Price);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize("OnlyAdmin")]
        public async Task<ActionResult<Guid>> DeletePost(Guid id)
        {
            return Ok(await _postsService.DeletePost(id));
        }

        
    }
}
