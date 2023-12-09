using GazpromNeftWebApi.Models;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GazpromNeftWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get(long? id = null)
        {
            var request = new GetUserRequest() { Id = id };
            var users = await _mediator.Send(request);
            return Ok(users);
        }

        [HttpPost(Name = "AddUser")]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            var userId = await _mediator.Send(request);
            return Ok(userId);
        }

        [HttpDelete(Name = "DeleteUser")]
        public async Task<IActionResult> Delete(DeleteUserRequest request)
        {
            var userId = await _mediator.Send(request);
            return NoContent();
        }
    }
}
