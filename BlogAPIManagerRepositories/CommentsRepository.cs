using AutoMapper;
using BlogAPIData;
using BlogAPIModels.DtoModels;
using BlogAPIModels.Entities;
using Microsoft.EntityFrameworkCore;

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
            var comments = await _context.Comments.Where(c => c.PostId == postId).Include(p => p.Author).ToListAsync();
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
        public async Task<int> CreateComment(int postId, CommentDto dto, int userId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);

            if (post is null)
                throw new Exception("Post not found");

            var comment = new Comment()
            {
                Text = dto.Text,
                PostId = postId,
                AuthorId = userId,
            };

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

        public async Task<int> DeleteComment(int postId, int id, int userId)
        {
            var existingComment = _context.Comments.SingleOrDefault(c => c.PostId == postId && c.Id == id && c.AuthorId == userId);
            if (existingComment is null) return -1;

            await Task.Run(() => { _context.Comments.Remove(existingComment); });
            await _context.SaveChangesAsync();
            return existingComment.Id;
        }
    }
}
