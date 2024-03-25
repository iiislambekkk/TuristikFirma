﻿using TuristikFirma.Models;

namespace TuristikFirma.Abstractions
{
    public interface IPostsService
    {
        Task<Guid> CreatePost(Post book);
        Task<Guid> DeletePost(Guid id);
        Task<List<Post>> GetAllPosts();
        Task<Guid> UpdatePost(Guid id, string title, string description, decimal price);
    }
}