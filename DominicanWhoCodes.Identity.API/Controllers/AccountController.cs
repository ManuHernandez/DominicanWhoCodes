
using DominicanWhoCodes.Identity.API.Models.Application.Commands;
using DominicanWhoCodes.Identity.API.Models.Application.DTO;
using DominicanWhoCodes.Identity.API.Models.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DominicanWhoCodes.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] NewUserDto newUser)
        {
            try
            {
                var userCreatedResult = await _mediator.Send(new NewUserCommand(newUser));

                return Ok();
            }
            catch (UserException userEx)
            {
                return BadRequest(userEx.Message);
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}