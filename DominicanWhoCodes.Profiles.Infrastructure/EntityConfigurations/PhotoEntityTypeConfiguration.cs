

using DominicanWhoCodes.Profiles.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DominicanWhoCodes.Profiles.Infrastructure.EntityConfigurations
{
    internal class PhotoEntityTypeConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photos", UserProfileContext.DEFAULT_SCHEMA);
            builder.HasKey(p => p.Id);
            builder.Ignore(p => p.PhotoId);
            builder.Ignore(p => p.UserId);
            builder.Property(p => p.FileName).IsRequired(true).HasMaxLength(250);
            builder.Property(p => p.ImageSource).IsRequired(true);
        }
    }
}
