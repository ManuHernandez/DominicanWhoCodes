

using MediatR;
using System;

namespace DominicanWhoCodes.ObjectStorage.MinioAPI.Models.Application.Command
{
    public class UploadProfilePhotoCommand : IRequest<bool>
    {
        public UploadProfilePhotoCommand(string fileName, byte[] photoContent, Guid userId)
        {
            FileName = fileName;
            PhotoContent = photoContent;
            UserId = userId;
        }
        public string FileName { get; private set; }
        public byte[] PhotoContent { get; private set; }
        public Guid UserId { get; private set; }
    }
}
