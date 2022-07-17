using BlogAPIModels.DtoModels;
using BlogAPIModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIServices
{
    public interface ICommentsService
    {
        Task<List<Comment>> GetAllComments(int postId);
        Task<CommentDto> GetComment(int postId, int id);
        Task<int> CreateComment(int postId, CommentDto comment);
        Task<int> UpdateComment(int postId, int id, CommentDto post);
        Task<int> DeleteComment(int postId, int id);
    }
}
