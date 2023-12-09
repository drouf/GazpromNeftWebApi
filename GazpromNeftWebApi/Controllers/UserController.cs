using GazpromNeftWebApi.Models;
using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;

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

        [HttpGet]
        [ProducesResponseType<IEnumerable<User>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status404NotFound)]
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

        [HttpPost]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status404NotFound)]
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

        [HttpPut]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status404NotFound)]
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status404NotFound)]
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
