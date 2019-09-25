

using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DominicanWhoCodes.Profiles.Infrastructure.EntityConfigurations
{
    internal class SocialNetworkEntityTypeConfiguration : IEntityTypeConfiguration<SocialNetwork>
    {
        public void Configure(EntityTypeBuilder<SocialNetwork> builder)
        {
            builder.ToTable("SocialNetworks", UserProfileContext.DEFAULT_SCHEMA);
            builder.HasKey(s => s.Id);
            builder.Ignore(s => s.SocialNetworkId);
            builder.Ignore(s => s.UserId);
            builder.Property(s => s.Url).IsRequired(true).HasMaxLength(250);
            builder.Property(s => s.Network).IsRequired(true);
            builder.HasOne<User>()
                .WithMany(u => u.SocialNetworks)
                .HasForeignKey("UserAssignedId")
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
