using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PADLab2_1part.Data;
using PADLab2_1part.Models;

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

        [HttpGet]
        public ActionResult<LikeCount> getLikes(string id)
        {
            var likesItems = _repo.GetNumberOfLikes(id);
            return Ok(likesItems);
        }

        [HttpGet("{id}/users")]
        public ActionResult<IEnumerable<User>> getLikesUsers(string id)
        {
            var likesItems = _repo.GetLikesUsers(id);
            return Ok(likesItems.AsEnumerable());
        }

        [HttpPost]
        public ActionResult addLike(Like like)
        {
            _repo.AddLike(like);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult removeLike(Like like)
        {
            _repo.DeleteLike(like);
            return NoContent();
        }
    }
}
