﻿using BlogAPIModels;
using BlogAPIModels.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIServices
{
    public interface IPostsService
    {
        Task<List<Post>> GetAllPosts();
        Task<Post?> GetPostById(int postId);
        Task<int> CreatePost(PostDto post, int userId);
        Task<int> UpdatePost(int id, PostDto post);
        Task<int> DeletePost(int postId);
    }
}