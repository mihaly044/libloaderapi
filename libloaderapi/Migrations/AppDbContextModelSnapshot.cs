﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using libloaderapi.Domain.Database;

namespace libloaderapi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("libloaderapi.Domain.Database.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("varchar(172)");

                    b.Property<int>("BucketType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Digest")
                        .IsRequired()
                        .HasColumnType("varchar(28)")
                        .HasMaxLength(28);

                    b.Property<DateTime?>("LastUsed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RegistrantIp")
                        .IsRequired()
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Tag")
                        .HasColumnType("varchar(32)")
                        .HasMaxLength(32);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("libloaderapi.Domain.Database.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e11d2d77-3d5e-4f03-96b2-ec8d8ba0c7c8"),
                            Name = "LibAdmin"
                        },
                        new
                        {
                            Id = new Guid("8837a565-49ff-423e-86cb-bcbd5af487d7"),
                            Name = "LibUser"
                        },
                        new
                        {
                            Id = new Guid("ceb6ae8f-62e8-4e1a-9017-fa7a4e01ab52"),
                            Name = "LibClient"
                        });
                });

            modelBuilder.Entity("libloaderapi.Domain.Database.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c719978f-e75f-4ec3-b55b-cd80840e325d"),
                            CreatedAt = new DateTime(2020, 9, 13, 22, 42, 28, 787, DateTimeKind.Utc).AddTicks(6415),
                            Name = "admin",
                            Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
                        },
                        new
                        {
                            Id = new Guid("0a40b8f2-3273-422c-b12f-3ac41feb6313"),
                            CreatedAt = new DateTime(2020, 9, 13, 22, 42, 28, 787, DateTimeKind.Utc).AddTicks(7398),
                            Name = "user",
                            Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
                        });
                });

            modelBuilder.Entity("libloaderapi.Domain.Database.Models.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("c719978f-e75f-4ec3-b55b-cd80840e325d"),
                            RoleId = new Guid("e11d2d77-3d5e-4f03-96b2-ec8d8ba0c7c8")
                        },
                        new
                        {
                            UserId = new Guid("0a40b8f2-3273-422c-b12f-3ac41feb6313"),
                            RoleId = new Guid("8837a565-49ff-423e-86cb-bcbd5af487d7")
                        });
                });

            modelBuilder.Entity("libloaderapi.Domain.Database.Models.Client", b =>
                {
                    b.HasOne("libloaderapi.Domain.Database.Models.User", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("libloaderapi.Domain.Database.Models.UserRole", b =>
                {
                    b.HasOne("libloaderapi.Domain.Database.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("libloaderapi.Domain.Database.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
