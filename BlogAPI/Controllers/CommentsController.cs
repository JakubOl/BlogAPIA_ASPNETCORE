using BlogAPIModels.DtoModels;
using BlogAPIServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
	[Authorize]
    [Route("/posts/{postId}/comments")]
    public class CommentsController: Controller
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllComments(int? postId)
        {
            if (postId == null)
            {
                return NotFound();
            }
            var posts = await _commentsService.GetAllComments((int)postId);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetComment(int? postId, int? id)
        {
            if (postId == null || id == null)
            {
                return NotFound();
            }
            var post = await _commentsService.GetComment((int)postId, (int)id);
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult> Create(int? postId, [FromForm] CommentDto dto)
        {
            if (postId == null)
            {
				return NotFound();
            }

            var userId = User.FindFirstValue("userId");

            if (userId is null)
            {
                return Redirect("/register");
            }

            await _commentsService.CreateComment((int)postId, dto, int.Parse(userId));
            return Redirect($"/posts/{postId}");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? postId, int? id, [FromForm] CommentDto dto)
        {
            if (id == null || postId == null)
            {
                return NotFound();
            }

            await _commentsService.UpdateComment((int)postId, (int)id, dto);
            return Ok();
        }

        [HttpPost("{id}/delete")]
        public async Task<ActionResult> Delete(int postId, int id)
        {
            if (id == null || postId == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue("userId");

            if (userId is null)
            {
                return Redirect("/register");
            }

            var commentId = await _commentsService.DeleteComment(postId, id, int.Parse(userId));
            if(commentId == -1)
            {
                return NotFound();
            }

            return Redirect($"/posts/{postId}");
        }
    }
}
