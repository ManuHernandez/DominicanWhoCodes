
using DominicanWhoCodes.Profiles.API.Application.Commands;
using DominicanWhoCodes.Profiles.API.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DominicanWhoCodes.Profiles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserUploadPhotosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserUploadPhotosController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(Guid userId, IFormFile photo)
        {
            try
            {
                await _mediator.Send(new UploadUserProfilePhotoCommand(userId, photo.FileName, 
                    FormFileToByte(photo)));
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        private byte[] FormFileToByte(IFormFile formFile)
        {
            if (formFile == null) return null;
            using (var stream = new MemoryStream())
            {
                formFile.CopyTo(stream);
                var fileByte = stream.ToArray();
                return fileByte;
            }
        }
    }
}