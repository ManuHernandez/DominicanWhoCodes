

using MediatR;

namespace DominicanWhoCodes.Profiles.Domain.Aggregates.Users.Events
{
    public class UploadNewPhotoFromFileSystemDomainEvent : INotification
    {
        public UploadNewPhotoFromFileSystemDomainEvent(UserId userId, string fileName, 
            byte[] uploadContent)
        {
            UserId = userId;
            FileName = fileName;
            UploadContent = uploadContent;
        }
        public string FileName { get; private set; }
        public byte[] UploadContent { get; private set; }
        public UserId UserId { get; private set; }
    }
}
