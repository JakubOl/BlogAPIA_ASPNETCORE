using BlogAPIManagerRepositories;
using BlogAPIModels;
using BlogAPIModels.DtoModels;

namespace BlogAPIServices
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;

        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }
        public async Task<PagedResult<Post>> GetAllPosts(PostQuery query)
        {
            return await _postsRepository.GetAllPosts(query);
        }
        public async Task<List<Post>> GetUserPosts(int userId)
        {
            return await _postsRepository.GetUserPosts(userId);
        }
        public async Task<Post?> GetPostById(int postId)
        {
            return await _postsRepository.GetPostById(postId);
        }
        
        public async Task<int> CreatePost(PostDto post, int userId)
        {
            return await _postsRepository.CreatePost(post, userId);
        }
        public async Task<int> UpdatePost(int id, PostDto post)
        {
            return await _postsRepository.UpdatePost(id, post);
        }

        public async Task<int> DeletePost(int postId, int userId)
        {
            return await _postsRepository.DeletePost(postId, userId);
        }

        
    }
}