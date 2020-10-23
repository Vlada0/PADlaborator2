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
        public async Task<ActionResult> GetPictures()
        {
            var picturesItems = await _repo.GetPictures();
           // Console.WriteLine(picturesItems);
            return Ok(picturesItems.AsEnumerable()); 
        }

        [HttpGet("{id}", Name = "GetPicture")]
        public async Task<ActionResult<Picture>> GetPicture(Guid id)
        {
            var picturesItem = await _repo.GetPictureById(id);
  
            return Ok(picturesItem);
        }

        [HttpPost]
        public async Task<ActionResult <Picture>> Post(Picture picture)
        {
            var _picture = await _repo.CreatePicture(picture);
            return CreatedAtRoute(routeName: "GetPicture", routeValues: new {id = picture.Id}, value: picture);
        }

        [HttpPut]
        public async Task<ActionResult<Picture>> Put(Picture picture)
        {
            var _picture = await _repo.Update(picture);

            return Ok(_picture);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Picture>> Delete(Guid id)
        {
            await _repo.Delete(id);

            return NoContent();
        }
    }
}

