

using System.Threading;
using System.Threading.Tasks;
using DominicanWhoCodes.Profiles.API.Application.Exceptions;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users.Events;
using MediatR;

namespace DominicanWhoCodes.Profiles.API.Application.DomainEvents
{
    public class UploadNewPhotoFromFileSystemDomainEventHandler : INotificationHandler<UploadNewPhotoFromFileSystemDomainEvent>
    {
        public async Task Handle(UploadNewPhotoFromFileSystemDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.UploadContent == null)
                throw new UploadPhotoNullException("Upload photo cannot be null.");
        }
    }
}
