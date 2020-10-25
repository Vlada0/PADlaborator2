using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PADLab2_1part.Data;
using PADLab2_1part.Models;

using Microsoft.Extensions.Logging;
using PADLab2_1part.Services;

namespace PADLab2_1part.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _service;

        public LikesController(ILikeService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LikeCount>> getLikes(Guid id)
        {
            var likesItems = await _service.GetNumberOfLikes(id);
            return Ok(likesItems);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<User>>> getLikesUsers(Guid id)
        {
            var likesItems = await _service.GetLikesUsers(id);
            return Ok(likesItems.AsEnumerable());
        }

        [HttpPost]
        public async Task<ActionResult> addLike(Like like)
        {
            await _service.AddLike(like);
            return new ObjectResult(like) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete]
        public async Task<ActionResult> removeLike(Like like)
        {
            await _service.DeleteLike(like);
            return NoContent();
        }
    }
}
