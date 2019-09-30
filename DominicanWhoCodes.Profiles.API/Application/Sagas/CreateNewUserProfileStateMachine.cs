

using Automatonymous;
using DominicanWhoCodes.Profiles.API.Application.IntegrationEvents;
using DominicanWhoCodes.Profiles.API.Application.IntegrationEvents.Definitions;
using System;

namespace DominicanWhoCodes.Profiles.API.Application.Sagas
{
    public class CreateNewUserProfileStateMachine :
        MassTransitStateMachine<UserSagaInstance>
    {
        protected CreateNewUserProfileStateMachine()
        {
            InstanceState(e => e.CurrentState);

            Event(() => UserSubmitted, e => 
                e.CorrelateById(context => context.Message.User.Id));

            Event(() => UserCreationFailed, 
                e => e.CorrelateById(context => context.Message.User.Id));

            Initially(
                When(UserSubmitted)
                    .Then(context =>
                    {
                        context.Instance.FirstName = context.Data.User.FirstName;
                        context.Instance.LastName = context.Data.User.LastName;
                        context.Instance.Id = context.Data.User.Id;
                        context.Instance.Email = context.Data.User.Email;
                        context.Instance.Photo = context.Data.User.Photo;
                        context.Instance.SocialNetworks = context.Data.User.SocialNetworks;
                        context.Instance.Description = context.Data.User.Description;
                    })
                    .ThenAsync(context =>
                    {
                        return Console
                            .Out
                            .WriteLineAsync($"User Submitted: {context.Data.User.Id} " +
                                $"to {context.Instance.Id}");
                    })
                    .Publish(context => new CreateNewUserProfileIntegrationEvent(context.Instance))
                    .TransitionTo(Created));

            During(Created,
                When(UserCreationFailed)
                    .Then(context =>
                    {
                        context.Instance.FirstName = context.Data.User.FirstName;
                        context.Instance.LastName = context.Data.User.LastName;
                        context.Instance.Id = context.Data.User.Id;
                        context.Instance.Email = context.Data.User.Email;
                        context.Instance.Photo = context.Data.User.Photo;
                        context.Instance.SocialNetworks = context.Data.User.SocialNetworks;
                        context.Instance.Description = context.Data.User.Description;
                    })
                    .ThenAsync(context =>
                    {
                        return Console.Out.WriteLineAsync($"User Creation Failed: {context.Data.User.Id}");
                    })
                    .Publish(context => new CreationUserFailedIntegrationEvent(context.Instance))
                    .Finalize());

            SetCompletedWhenFinalized();
        }
        public State Initialize { get; private set; }
        public State Created { get; private set; }
        public Event<UserSubmitted> UserSubmitted { get; private set; }
        public Event<UserCreationFailed> UserCreationFailed { get; private set; }

    }
}
