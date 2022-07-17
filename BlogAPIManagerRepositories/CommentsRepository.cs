using AutoMapper;
using BlogAPIData;
using BlogAPIModels.DtoModels;
using BlogAPIModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIRepositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly BlogAPIDbContext _context;
        private readonly IMapper _mapper;

        public CommentsRepository(BlogAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Comment>> GetAllComments(int postId)
        {
            var comments = await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
            if (comments == null) throw new Exception("Could not Get Comments due to unable to find");

            return comments;
        }
        public async Task<CommentDto> GetComment(int postId, int id)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(c => c.PostId == postId && c.Id == id);
            if (comment == null) throw new Exception("Could not Get Comment due to unable to find");

            var commentDto = _mapper.Map<CommentDto>(comment);

            return commentDto;
        }
        public async Task<int> CreateComment(int postId, CommentDto dto)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

            if (post is null)
                throw new Exception("Restaurant not found");

            var comment = _mapper.Map<Comment>(dto);

            comment.PostId = postId;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment.Id;
        }
        public async Task<int> UpdateComment(int postId, int id, CommentDto post)
        {
            var existingComment = _context.Comments.SingleOrDefault(c => c.PostId == postId && c.Id == id);
            if (existingComment is null) throw new Exception("Could not Update Comment due to unable to find");

            existingComment.Text = post.Text;

            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<int> DeleteComment(int postId, int id)
        {
            var existingComment = _context.Comments.SingleOrDefault(c => c.PostId == postId && c.Id == id);
            if (existingComment is null) throw new Exception("Could not Delete Comment due to unable to find");

            await Task.Run(() => { _context.Comments.Remove(existingComment); });
            await _context.SaveChangesAsync();
            return existingComment.Id;
        }

       
    }
}
