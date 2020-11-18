using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PADLab2_1part.Data;
using PADLab2_1part.Models;
using PADLab2_1part.Services;

namespace PADLab2_1part.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
       
        private readonly IPictureService _service;

        public PictureController(IPictureService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetPictures()
        {
            var picturesItems = await _service.GetPictures();
            Thread.Sleep(20000);
           // Console.WriteLine(picturesItems);
            return Ok(picturesItems.AsEnumerable()); 
        }

        [HttpGet("{id}", Name = "GetPicture")]
        public async Task<ActionResult<Picture>> GetPicture(Guid id)
        {
            var picturesItem = await _service.GetPictureById(id);
  
            return Ok(picturesItem);
        }

        [HttpPost]
        public async Task<ActionResult <Picture>> Post(Picture picture)
        {
            Thread.Sleep(5000);
            var _picture = await _service.CreatePicture(picture);
            return CreatedAtRoute(routeName: "GetPicture", routeValues: new {id = picture.Id}, value: picture);
        }

        [HttpPut]
        public async Task<ActionResult<Picture>> Put(Picture picture)
        {
            var _picture = await _service.Update(picture);

            return Ok(_picture);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Picture>> Delete(Guid id)
        {
            await _service.Delete(id);

            return NoContent();
        }
    }
}

