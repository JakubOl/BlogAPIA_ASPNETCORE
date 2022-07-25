using BlogAPIModels;
using BlogAPIModels.DtoModels;

namespace BlogAPIServices
{
    public interface IPostsService
    {
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetUserPosts(int userId);
        Task<Post?> GetPostById(int postId);
        Task<int> CreatePost(PostDto post, int userId);
        Task<int> UpdatePost(int id, PostDto post);
        Task<int> DeletePost(int postId, int userId);
    }
}
