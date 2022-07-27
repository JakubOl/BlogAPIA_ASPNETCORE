using BlogAPIModels;
using BlogAPIModels.DtoModels;
using BlogAPIServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    [Route("/posts")]
    [ApiController]
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostsService _postsService;
        private readonly IAccountService _accountService;

        public PostsController(IPostsService postsService, IAccountService accountService)
        {
            _postsService = postsService;
            _accountService = accountService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllPosts([FromQuery] PostQuery query)
        {
            var posts =  await _postsService.GetAllPosts(query);
            return View("Posts", posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue("userId");

            var post = await _postsService.GetPostById((int)id);
            if (post is null)
            {
                return NotFound();
            }
            ViewData["userId"] = userId;
            return View("Post", post);
        }

        [HttpGet("myPosts")]
        public async Task<ActionResult> GetUserPosts()
        {
            var userId = User.FindFirstValue("userId");

            if (userId is null)
            {
                return Redirect("/register");
            }

            var posts = await _postsService.GetUserPosts(int.Parse(userId));
            return View("MyPosts", posts);
        }

        [HttpGet("new")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm]PostDto dto)
        {
            var userId = User.FindFirstValue("userId");

            if (userId is null)
            {
                return Redirect("/register");
            }

            await _postsService.CreatePost(dto, int.Parse(userId));
            return Redirect("/posts");
        }

        [HttpGet("{id}/edit")]
        public async Task<ActionResult> Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue("userId");

            if (userId is null)
            {
                return Redirect("/register");
            }

            var post = await _postsService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            if (post.AuthorId != int.Parse(userId))
            {
                return Redirect("/posts/new");
            }
            return View("Edit", post);
        }

        [HttpPost("{id}/edit")]
        public async Task<ActionResult> Update(int? id, [FromForm]PostDto dto)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postId = await _postsService.UpdatePost((int)id, dto);
            return Redirect($"/posts/{postId}");
        }

        [HttpPost("{id}/delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue("userId");

            if (userId is null)
            {
                return Redirect("/register");
            }

            var postId = await _postsService.DeletePost((int)id, int.Parse(userId));
            if(postId == -1)
            {
                TempData["Error"] = "You are not allowed to do this";
                return Redirect("posts/new");
            }
            return Redirect("/posts/myPosts");
        }
    }
}
