using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PADLab2_1part.Data;
using PADLab2_1part.Models;

namespace PADLab2_1part.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
       
        private readonly IPictureRepo _repo;

        public PictureController(IPictureRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult GetPictures()
        {
            var picturesItems = _repo.GetPictures();
            Console.WriteLine(picturesItems);
            return Ok(picturesItems.AsEnumerable()); 
        }

        [HttpGet("{id}")]
        public ActionResult<Picture> GetPicture(string id)
        {
            var picturesItem = _repo.GetPictureById(id);

            return Ok(picturesItem);
        }

        [HttpPost]
        public ActionResult <Picture> Post(Picture picture)
        {
            var _picture = _repo.CreatePicture(picture);
            return Ok(_picture);
        }

        [HttpPut]
        public ActionResult<Picture> Put(Picture picture)
        {
            var _picture = _repo.Update(picture);

            return Ok(_picture);
        }


        [HttpDelete("{id}")]
        public ActionResult<Picture> Delete(string id)
        {
             _repo.Delete(id);

            return NoContent();
        }
    }
}

