using BlogAPIModels.DtoModels;
using BlogAPIServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Controllers
{
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
        public async Task<ActionResult> Create(int? postId, [FromBody] CommentDto dto)
        {
            if (postId == null)
            {
                return NotFound();
            }
            await _commentsService.CreateComment((int)postId, dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? postId, int? id, [FromBody] CommentDto dto)
        {
            if (id == null || postId == null)
            {
                return NotFound();
            }

            await _commentsService.UpdateComment((int)postId, (int)id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? postId, int? id)
        {
            if (id == null || postId == null)
            {
                return NotFound();
            }

            await _commentsService.DeleteComment((int)postId, (int)id);
            return Ok();
        }
    }
}
