using GazpromNeftWebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using GazpromNeftWebApi.DTO;

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
        [ProducesResponseType<IEnumerable<UserDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery]GetUserRequest request)
        {
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
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateUserRequest request)
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

        [HttpPatch]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(PatchUserRequest request)
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<IEnumerable<ValidationFailure>>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteUserRequest request)
        {
            try
            {
                await _mediator.Send(request);
                return NoContent();
            }
            catch (FluentValidation.ValidationException validationException)
            {
                return BadRequest(validationException.Errors);
            }
        }
    }
}
