
using DominicanWhoCodes.Identity.API.Models.Application.Commands;
using DominicanWhoCodes.Identity.API.Models.Application.InputModels;
using DominicanWhoCodes.Identity.API.Models.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
       
        public async Task<IActionResult> Register([FromBody] NewUserInputModel newUser)
        {
            try
            {
                var userProfileDto = newUser.ConvertToUserProfile();
                var userCreatedResult = await _mediator.Send(new NewUserCommand(userProfileDto, newUser.Password));
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