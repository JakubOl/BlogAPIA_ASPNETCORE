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
        public async Task<PagedResult<Post>> GetAllPosts(PostQuery query)
        {
            var baseQuery = await _context.Posts
                .Include(p => p.Comments)
                .Where(p => p.IsPublished == true)
                .Where(p => query.SearchPhrase == null || (p.Text.ToLower().Contains(query.SearchPhrase.ToLower()) || p.Title.ToLower().Contains(query.SearchPhrase.ToLower())))
                .ToListAsync();


            int skipPage = query.PageSize * (query.PageNumber - 1);

            var pagedResult = baseQuery.Skip(skipPage).Take(query.PageSize).ToList();

            var totalItems = baseQuery.Count();

            var posts = new PagedResult<Post>(pagedResult, totalItems, query.PageSize, query.PageNumber);

            return posts;
        }
        public async Task<List<Post>> GetUserPosts(int userId)
        {
            var posts = await _context.Posts.Include(p => p.Comments).Where(p => p.AuthorId == userId).ToListAsync();
            return posts;
        }

        public async Task<Post?> GetPostById(int postId)
        {
            var post = await _context.Posts.Include(p => p.Author).Include(p => p.Comments).ThenInclude(p => p.Author).FirstOrDefaultAsync(p => p.Id == postId);

            return post;
        }

        public async Task<int> CreatePost(PostDto dto, int userId)
        {
            var newPost = new Post()
            {
                Title = dto.Title,
                Text = dto.Text,
                IsPublished = dto.IsPublished,
                AuthorId = userId,
            };


            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
            return newPost.Id;
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
        public async Task<int> DeletePost(int id, int userId)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPost is null) throw new Exception("Post not found");

            if(existingPost.AuthorId != userId)
            {
                return -1;
            }

            _context.Posts.Remove(existingPost);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}