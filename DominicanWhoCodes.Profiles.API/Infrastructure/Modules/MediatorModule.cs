

using Autofac;
using DominicanWhoCodes.Profiles.API.Application.Commands;
using DominicanWhoCodes.Profiles.API.Application.DomainEvents;
using MediatR;
using System.Reflection;

namespace DominicanWhoCodes.Profiles.API.Infrastructure.Modules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
               .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(CreateNewUserProfileCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(UploadNewPhotoFromFileSystemDomainEventHandler)
                .GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
