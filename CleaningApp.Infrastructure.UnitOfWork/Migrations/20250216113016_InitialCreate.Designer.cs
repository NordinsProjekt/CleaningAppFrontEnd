﻿// <auto-generated />
using System;
using CleaningApp.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleaningApp.Infrastructure.UnitOfWork.Migrations
{
    [DbContext(typeof(CleaningDBContext))]
    [Migration("20250216113016_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CleaningApp.Domain.Entities.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("88583256-aec1-49ef-ac92-bea090ebd566"),
                            Name = "Vardagsrum"
                        },
                        new
                        {
                            Id = new Guid("f63ce709-dced-452e-857b-d8ea4804650f"),
                            Name = "Kök"
                        },
                        new
                        {
                            Id = new Guid("1b234aab-fa95-48bc-9cf0-99476989d396"),
                            Name = "Badrum uppe"
                        },
                        new
                        {
                            Id = new Guid("aa1b40e0-21c7-4eba-ae7c-0d782e6c1829"),
                            Name = "Badrum nere"
                        },
                        new
                        {
                            Id = new Guid("128b480f-40fe-4785-af35-0ea3a9c69aa5"),
                            Name = "Sovrum"
                        });
                });

            modelBuilder.Entity("CleaningApp.Domain.Entities.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("TaskDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TaskTypeId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("TaskTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("CleaningApp.Domain.Entities.TaskType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TaskTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f578b79f-b5fc-44f7-9ca3-c6659b26f07e"),
                            Name = "Dammsugit golv"
                        },
                        new
                        {
                            Id = new Guid("027035be-5cc0-4dc8-b72e-e9f5802ce147"),
                            Name = "Tvättat golv"
                        },
                        new
                        {
                            Id = new Guid("92e9431e-38d8-4154-82c6-6ab5b1189ceb"),
                            Name = "Torkat av alla ytor"
                        },
                        new
                        {
                            Id = new Guid("5c3beef8-87b4-4dd3-9501-5830cd2e7b90"),
                            Name = "Rengjort badrum"
                        },
                        new
                        {
                            Id = new Guid("238e2350-8d9c-41f6-9a23-a0aa0f33addf"),
                            Name = "Bytt sängkläder"
                        },
                        new
                        {
                            Id = new Guid("cd66f083-04b2-4d2c-a89f-6ea3124590be"),
                            Name = "Tömt sopor"
                        },
                        new
                        {
                            Id = new Guid("fb02076e-1d95-46bc-aab5-992e3a6d88a8"),
                            Name = "Tömt tvättmaskin"
                        },
                        new
                        {
                            Id = new Guid("81a69c4f-b985-4fb2-8afb-24e84aafb3a1"),
                            Name = "Startat tvättmaskin"
                        },
                        new
                        {
                            Id = new Guid("171f7583-1b33-4ad7-bb72-247afab191e8"),
                            Name = "Tömt diskmaskin"
                        },
                        new
                        {
                            Id = new Guid("e0566d9f-3489-4af2-8af0-afaa6e2ff289"),
                            Name = "Startat diskmaskin"
                        });
                });

            modelBuilder.Entity("CleaningApp.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("adc2d573-3bc7-452a-89a1-459d1488f72b"),
                            Name = "Markus"
                        },
                        new
                        {
                            Id = new Guid("a44cdbdc-7af8-4b43-8b66-cda9e72f0c9a"),
                            Name = "Cecilia"
                        });
                });

            modelBuilder.Entity("CleaningApp.Domain.Entities.Task", b =>
                {
                    b.HasOne("CleaningApp.Domain.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleaningApp.Domain.Entities.TaskType", "TaskType")
                        .WithMany()
                        .HasForeignKey("TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleaningApp.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("TaskType");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
