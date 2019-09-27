using DominicanWhoCodes.ObjectStorage.MinioAPI.Models.Application.Command;
using DominicanWhoCodes.Shared.IntegrationEvents.Storage;
using MassTransit;
using MediatR;
using System.Threading.Tasks;

namespace DominicanWhoCodes.ObjectStorage.MinioAPI.Models.Application.IntegrationEvents.Consumers
{
    public class UploadProfilePhotoIntegrationEventHandler : IConsumer<UploadUserProfilePhotoIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public UploadProfilePhotoIntegrationEventHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public async Task Consume(ConsumeContext<UploadUserProfilePhotoIntegrationEvent> context)
        {
            await _mediator.Send(new UploadProfilePhotoCommand(context.Message.FileName, 
                context.Message.PhotoContent, context.Message.UserId));
        }
    }
}
