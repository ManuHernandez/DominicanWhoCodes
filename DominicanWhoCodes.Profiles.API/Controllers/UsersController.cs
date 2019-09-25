
using DominicanWhoCodes.Profiles.API.Application.Commands;
using DominicanWhoCodes.Shared.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DominicanWhoCodes.Profiles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserProfileDto userProfile)
        {
            try
            {
                await _mediator.Send(new CreateNewUserProfileCommand(userProfile));
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}