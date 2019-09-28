using System.Threading.Tasks;
using DominicanWhoCodes.Profiles.API.Application.Commands;
using MassTransit;
using MediatR;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Consumers
{
    public class CreateNewUserProfileIntegrationEventHandler : IConsumer<CreateNewUserProfileIntegrationEvent>
    {
        private readonly IMediator _mediator;
        public CreateNewUserProfileIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<CreateNewUserProfileIntegrationEvent> @event)
        {
            await _mediator.Send(new CreateNewUserProfileCommand(@event.Message.UserProfile));
        }
    }
}
