﻿// <auto-generated />
using System;
using CleaningApp.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleaningApp.Infrastructure.UnitOfWork.Migrations
{
    [DbContext(typeof(CleaningDBContext))]
    partial class CleaningDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("c77e0e61-91e4-4beb-970f-2d471036f9c9"),
                            Name = "Vardagsrum"
                        },
                        new
                        {
                            Id = new Guid("8fc9de16-fcee-4528-819d-1fbda6b49d62"),
                            Name = "Kök"
                        },
                        new
                        {
                            Id = new Guid("bf297c42-bb01-4a06-9033-c2ac438aab79"),
                            Name = "Badrum uppe"
                        },
                        new
                        {
                            Id = new Guid("15cb26d9-5f4a-4eef-9bf8-1c8bf09c7e03"),
                            Name = "Badrum nere"
                        },
                        new
                        {
                            Id = new Guid("7db467f3-275e-41ff-b01c-9ffe8eb83863"),
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

            modelBuilder.Entity("CleaningApp.Domain.Entities.TaskTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<Guid?>("DefaultUserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("char(36)");

                    b.Property<int>("TaskDuration")
                        .HasColumnType("int");

                    b.Property<Guid>("TaskTypeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DefaultUserId");

                    b.HasIndex("RoomId");

                    b.HasIndex("TaskTypeId");

                    b.ToTable("TaskTemplates");
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
                            Id = new Guid("59106689-5995-4ff9-a832-e8f4d8f20b01"),
                            Name = "Dammsugit golv"
                        },
                        new
                        {
                            Id = new Guid("9b2bb5da-dfc2-4d37-9964-e4df38d0aa30"),
                            Name = "Tvättat golv"
                        },
                        new
                        {
                            Id = new Guid("ebab6358-9109-4dbc-950e-a38ff1daeb0d"),
                            Name = "Torkat av alla ytor"
                        },
                        new
                        {
                            Id = new Guid("df494896-13e4-427d-9b2a-59c63484b935"),
                            Name = "Rengjort badrum"
                        },
                        new
                        {
                            Id = new Guid("5387d01a-8bd2-45ac-8bca-f294583d6430"),
                            Name = "Bytt sängkläder"
                        },
                        new
                        {
                            Id = new Guid("71915d86-90fa-428b-83ef-1bf6c185a829"),
                            Name = "Tömt sopor"
                        },
                        new
                        {
                            Id = new Guid("c19b9818-3aa8-4056-96f9-f5b3635d84f1"),
                            Name = "Tömt tvättmaskin"
                        },
                        new
                        {
                            Id = new Guid("fad31a36-d337-4402-bcf2-6f88054cfbc9"),
                            Name = "Startat tvättmaskin"
                        },
                        new
                        {
                            Id = new Guid("caa75b45-475a-4141-af5d-c8f9919336d8"),
                            Name = "Tömt diskmaskin"
                        },
                        new
                        {
                            Id = new Guid("978e053d-d3d0-447a-9c86-89050a908ee8"),
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
                            Id = new Guid("b8dc0358-577d-4839-9c08-e652ba8d465b"),
                            Name = "Markus"
                        },
                        new
                        {
                            Id = new Guid("e9868e20-8f1b-4c33-9437-85d2c516c1ee"),
                            Name = "Cecilia"
                        },
                        new
                        {
                            Id = new Guid("992d047b-d0cc-4976-8678-ebd3f4d914a0"),
                            Name = "Planerad"
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

            modelBuilder.Entity("CleaningApp.Domain.Entities.TaskTemplate", b =>
                {
                    b.HasOne("CleaningApp.Domain.Entities.User", "DefaultUser")
                        .WithMany()
                        .HasForeignKey("DefaultUserId");

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

                    b.Navigation("DefaultUser");

                    b.Navigation("Room");

                    b.Navigation("TaskType");
                });
#pragma warning restore 612, 618
        }
    }
}
