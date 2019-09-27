using MediatR;
using System;

namespace DominicanWhoCodes.Profiles.API.Application.Commands
{
    public class UploadUserProfilePhotoCommand : IRequest<bool>
    {
        public Guid UserId { get; private set; }
        public string FileName { get; private set; }
        public byte[] Photo { get; private set; }

        public UploadUserProfilePhotoCommand(Guid userId, string fileName, byte[] photo)
        {
            this.UserId = userId;
            this.FileName = fileName;
            this.Photo = photo;
        }
    }
}
