

using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DominicanWhoCodes.Profiles.Infrastructure.EntityConfigurations
{
    internal class SocialNetworkEntityTypeConfiguration : IEntityTypeConfiguration<SocialNetwork>
    {
        public void Configure(EntityTypeBuilder<SocialNetwork> builder)
        {
            builder.ToTable("SocialNetworks", UserProfileContext.DEFAULT_SCHEMA);
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Url).IsRequired(true).HasMaxLength(250);
            builder.Property(s => s.Network).IsRequired(true);
        }
    }
}
