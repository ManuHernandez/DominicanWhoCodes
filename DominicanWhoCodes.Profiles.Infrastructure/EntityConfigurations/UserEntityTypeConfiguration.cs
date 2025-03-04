﻿using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DominicanWhoCodes.Profiles.Infrastructure.EntityConfigurations
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", UserProfileContext.DEFAULT_SCHEMA);
            builder.HasKey(u => u.Id);
            builder.Ignore(u => u.UserId);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(80);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(250);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
            builder.Property(u => u.CreationDate).IsRequired();
            builder.Property(u => u.CurrentStatus).IsRequired().HasColumnName("Status");
            builder.Property(u => u.Description).HasMaxLength(2000);
            builder.HasOne(u => u.CurrentPhoto)
                .WithOne(u => u.User)
                .HasForeignKey(typeof(Photo), "UserAssignedId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
