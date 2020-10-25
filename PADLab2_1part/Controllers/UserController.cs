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
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;

        public UserController(IUserRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var usersItems = await _repo.GetUsers();
            // Console.WriteLine(picturesItems);
            return Ok(usersItems.AsEnumerable());
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var usersItem = await _repo.GetUserById(id);

            return Ok(usersItem);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            var _user = await _repo.CreateUser(user);
            return CreatedAtRoute(routeName: "GetUser", routeValues: new { id = user.UserId }, value: user);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            var _user = await _repo.UpdateUser(user);

            return Ok(_user);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(Guid id)
        {
            await _repo.DeleteUser(id);

            return NoContent();
        }
    }
}
