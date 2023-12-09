using GazpromNeftWebApi.Models;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            try
            {
                var users = await _mediator.Send(request);
                return Ok(users);
            }
            catch(FluentValidation.ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }

        [HttpPost(Name = "AddUser")]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            try
            {
                var user = await _mediator.Send(request);
                return Ok(user);
            }
            catch (FluentValidation.ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            try
            {
                var userId = await _mediator.Send(request);
                return Ok(userId);
            }
            catch (FluentValidation.ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }

        [HttpDelete(Name = "DeleteUser")]
        public async Task<IActionResult> Delete(DeleteUserRequest request)
        {
            try
            {
                var userId = await _mediator.Send(request);
                return NoContent();
            }
            catch (FluentValidation.ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }
    }
}
