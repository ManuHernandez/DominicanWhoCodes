

using System;
using System.Threading;
using System.Threading.Tasks;
using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using DominicanWhoCodes.Profiles.Infrastructure.EntityConfigurations;
using DominicanWhoCodes.Shared.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DominicanWhoCodes.Profiles.Infrastructure
{
    public class UserProfileContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public const string DEFAULT_SCHEMA = "UsersProfile";
        public UserProfileContext(DbContextOptions<UserProfileContext> dbContextOptions, IMediator mediator)
            :base(dbContextOptions)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        private UserProfileContext(DbContextOptions<UserProfileContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<Photo> Photos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SocialNetworkEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoEntityTypeConfiguration());
        }
        public async Task<bool> CommitChanges(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
