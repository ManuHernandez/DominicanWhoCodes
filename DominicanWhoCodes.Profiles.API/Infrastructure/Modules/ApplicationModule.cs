using Autofac;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using DominicanWhoCodes.Profiles.Infrastructure.Repositories;

namespace DominicanWhoCodes.Profiles.API.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
