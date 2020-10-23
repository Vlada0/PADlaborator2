using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PADLab2_1part.Data;
using PADLab2_1part.Models;

using Microsoft.Extensions.Logging;

namespace PADLab2_1part.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikesRepo _repo;

        public LikesController(ILikesRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LikeCount>> getLikes(Guid id)
        {
            var likesItems = await _repo.GetNumberOfLikes(id);
            return Ok(likesItems);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<User>>> getLikesUsers(Guid id)
        {
            var likesItems = await _repo.GetLikesUsers(id);
            return Ok(likesItems.AsEnumerable());
        }

        [HttpPost]
        public async Task<ActionResult> addLike(Like like)
        {
            await _repo.AddLike(like);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> removeLike(Like like)
        {
            await _repo.DeleteLike(like);
            return NoContent();
        }
    }
}
