﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using libloaderapi.Domain.Database;

namespace libloaderapi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200828220543_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("varchar(128)")
                        .HasMaxLength(128);

                    b.Property<DateTime?>("LastUsed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Sha256")
                        .IsRequired()
                        .HasColumnType("varchar(64)")
                        .HasMaxLength(64);

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
                            Id = new Guid("a6442220-61c1-42b9-83b3-5602948d99be"),
                            Name = "LibAdmin"
                        },
                        new
                        {
                            Id = new Guid("c5f85b3a-cf5d-4d07-9456-3cd07fc26501"),
                            Name = "LibUser"
                        },
                        new
                        {
                            Id = new Guid("7e84358d-7850-432f-8879-4bd1dab2d3f3"),
                            Name = "LibClient"
                        });
                });

            modelBuilder.Entity("libloaderapi.Domain.Database.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

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
                            Id = new Guid("ccc10522-6109-47df-beed-08280777b5a9"),
                            Name = "admin",
                            Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
                        },
                        new
                        {
                            Id = new Guid("698ed0ca-b489-4a8d-acaf-0d18c8a78d0c"),
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
                            UserId = new Guid("ccc10522-6109-47df-beed-08280777b5a9"),
                            RoleId = new Guid("a6442220-61c1-42b9-83b3-5602948d99be")
                        },
                        new
                        {
                            UserId = new Guid("698ed0ca-b489-4a8d-acaf-0d18c8a78d0c"),
                            RoleId = new Guid("c5f85b3a-cf5d-4d07-9456-3cd07fc26501")
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