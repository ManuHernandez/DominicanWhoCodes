﻿// <auto-generated />
using System;
using DominicanWhoCodes.Profiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DominicanWhoCodes.Profiles.API.Migrations
{
    [DbContext(typeof(UserProfileContext))]
    partial class UserProfileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DominicanWhoCodes.Profiles.Domain.Aggregates.Users.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("ImageSource");

                    b.Property<string>("Url");

                    b.Property<Guid?>("UserAssignedId");

                    b.HasKey("Id");

                    b.HasIndex("UserAssignedId")
                        .IsUnique()
                        .HasFilter("[UserAssignedId] IS NOT NULL");

                    b.ToTable("Photos","UsersProfile");
                });

            modelBuilder.Entity("DominicanWhoCodes.Profiles.Domain.Aggregates.Users.SocialNetwork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Network");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<Guid>("UserAssignedId");

                    b.HasKey("Id");

                    b.HasIndex("UserAssignedId");

                    b.ToTable("SocialNetworks","UsersProfile");
                });

            modelBuilder.Entity("DominicanWhoCodes.Profiles.Domain.Aggregates.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("CurrentStatus")
                        .HasColumnName("Status");

                    b.Property<string>("Description")
                        .HasMaxLength(2000);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Users","UsersProfile");
                });

            modelBuilder.Entity("DominicanWhoCodes.Profiles.Domain.Aggregates.Users.Photo", b =>
                {
                    b.HasOne("DominicanWhoCodes.Profiles.Domain.Aggregates.Users.User", "User")
                        .WithOne("CurrentPhoto")
                        .HasForeignKey("DominicanWhoCodes.Profiles.Domain.Aggregates.Users.Photo", "UserAssignedId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DominicanWhoCodes.Profiles.Domain.Aggregates.Users.SocialNetwork", b =>
                {
                    b.HasOne("DominicanWhoCodes.Profiles.Domain.Aggregates.Users.User")
                        .WithMany("SocialNetworks")
                        .HasForeignKey("UserAssignedId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
