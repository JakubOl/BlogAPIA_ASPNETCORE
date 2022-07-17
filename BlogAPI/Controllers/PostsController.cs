using BlogAPIModels;
using BlogAPIModels.DtoModels;
using BlogAPIServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPosts()
        {
            var posts =  await _postsService.GetAllPosts();
            //return View("Posts", posts);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postsService.GetPostById((int)id);
            //return View("Posts", posts);
            if (post is null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PostDto dto)
        {
            await _postsService.CreatePost(dto, 1);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, [FromBody]PostDto dto)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _postsService.UpdatePost((int)id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _postsService.DeletePost((int)id);
            return Ok();
        }
    }
}
