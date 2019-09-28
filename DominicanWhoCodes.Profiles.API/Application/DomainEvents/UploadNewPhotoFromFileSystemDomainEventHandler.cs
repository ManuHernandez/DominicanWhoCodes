

using System.Threading;
using System.Threading.Tasks;
using DominicanWhoCodes.Profiles.API.Application.Exceptions;
using DominicanWhoCodes.Profiles.API.Application.IntegrationEvents;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users.Events;
using MassTransit;
using MediatR;

namespace DominicanWhoCodes.Profiles.API.Application.DomainEvents
{
    public class UploadNewPhotoFromFileSystemDomainEventHandler : INotificationHandler<UploadNewPhotoFromFileSystemDomainEvent>
    {
        private readonly IBus _bus;

        public UploadNewPhotoFromFileSystemDomainEventHandler(IBus bus)
        {
            this._bus = bus;
        }
        public async Task Handle(UploadNewPhotoFromFileSystemDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.UploadContent == null)
                throw new UploadPhotoNullException("Upload photo cannot be null.");

            await _bus.Publish(new UploadUserProfilePhotoIntegrationEvent(notification.FileName,
                   notification.UploadContent, notification.UserId.Value));
        }
    }
}
