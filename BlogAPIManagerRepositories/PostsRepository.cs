using AutoMapper;
using BlogAPIData;
using BlogAPIModels;
using BlogAPIModels.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace BlogAPIManagerRepositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly BlogAPIDbContext _context;
        private readonly IMapper _mapper;

        public PostsRepository(BlogAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _context.Posts.Include(p => p.Comments).ToListAsync();
            return posts;
        }

        public async Task<Post?> GetPostById(int postId)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);

            return post;
        }

        public async Task<int> CreatePost(PostDto dto, int userId)
        {
            var post = _mapper.Map<Post>(dto);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post.Id;
        }

        public async Task<int> UpdatePost(int id, PostDto dto)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPost is null) throw new Exception("Post not found");

            existingPost.Title = dto.Title;
            existingPost.Text = dto.Text;
            existingPost.IsPublished = dto.IsPublished;

            await _context.SaveChangesAsync();
            return existingPost.Id;
        }
        public async Task<int> DeletePost(int id)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPost is null) throw new Exception("Post not found");

            _context.Posts.Remove(existingPost);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}