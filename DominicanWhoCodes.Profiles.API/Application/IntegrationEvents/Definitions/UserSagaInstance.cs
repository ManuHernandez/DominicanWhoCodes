

using Automatonymous;
using DominicanWhoCodes.Profiles.API.Application.DTO;
using System;

namespace DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Definitions
{
    public class UserSagaInstance : UserProfileDto,  SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
    }
}
