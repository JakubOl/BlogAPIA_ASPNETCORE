using BlogAPIModels.DtoModels;
using BlogAPIModels.Entities;
using BlogAPIRepositories;

namespace BlogAPIServices
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }
        public async Task<List<Comment>> GetAllComments(int postId)
        {
            return await _commentsRepository.GetAllComments(postId);
        }
        public async Task<CommentDto> GetComment(int postId, int id)
        {
            return await _commentsRepository.GetComment(postId, id);
        }
        public async Task<int> CreateComment(int postId, CommentDto comment, int userId)
        {
            return await _commentsRepository.CreateComment(postId, comment, userId);
        }
        public async Task<int> UpdateComment(int postId, int id, CommentDto post)
        {
            return await _commentsRepository.UpdateComment(postId, id, post);
        }

        public async Task<int> DeleteComment(int postId, int id, int userId)
        {
            return await _commentsRepository.DeleteComment(postId, id, userId);
        }
    }
}
