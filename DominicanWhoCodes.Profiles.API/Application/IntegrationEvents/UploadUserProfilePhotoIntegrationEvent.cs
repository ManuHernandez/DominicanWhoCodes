using DominicanWhoCodes.Shared.EventBus;
using System;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents
{
    public class UploadUserProfilePhotoIntegrationEvent : IntegrationEvent
    {
        public UploadUserProfilePhotoIntegrationEvent(string fileName, byte[] photoContent, Guid userId)
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
