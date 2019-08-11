﻿// <auto-generated />
using System;
using Fritz.ResourceManagement.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fritz.ResourceManagement.Web.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GivenName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("ScheduleId");

                    b.Property<string>("SurName");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.PersonPersonType", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("PersonTypeId");

                    b.HasKey("PersonId", "PersonTypeId");

                    b.HasIndex("PersonTypeId");

                    b.ToTable("PersonPersonType");
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.PersonType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PersonTypes");
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.RecurringSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CronPattern");

                    b.Property<TimeSpan>("Duration");

                    b.Property<DateTime>("MaxEndDateTime");

                    b.Property<DateTime>("MinStartDateTime");

                    b.Property<string>("Name");

                    b.Property<int>("ScheduleId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("RecurringSchedule");
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.ScheduleException", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDateTime");

                    b.Property<string>("Name");

                    b.Property<int?>("ScheduleId");

                    b.Property<DateTime>("StartDateTime");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleException");
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.ScheduleItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDateTime");

                    b.Property<string>("Name");

                    b.Property<int>("ScheduleId");

                    b.Property<DateTime>("StartDateTime");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleItem");
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.Person", b =>
                {
                    b.HasOne("Fritz.ResourceManagement.Domain.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.PersonPersonType", b =>
                {
                    b.HasOne("Fritz.ResourceManagement.Domain.Person", "Person")
                        .WithMany("PersonPersonType")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fritz.ResourceManagement.Domain.PersonType", "PersonType")
                        .WithMany("PersonPersonTypes")
                        .HasForeignKey("PersonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.RecurringSchedule", b =>
                {
                    b.HasOne("Fritz.ResourceManagement.Domain.Schedule", null)
                        .WithMany("RecurringSchedules")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.ScheduleException", b =>
                {
                    b.HasOne("Fritz.ResourceManagement.Domain.Schedule", null)
                        .WithMany("ScheduleExceptions")
                        .HasForeignKey("ScheduleId");
                });

            modelBuilder.Entity("Fritz.ResourceManagement.Domain.ScheduleItem", b =>
                {
                    b.HasOne("Fritz.ResourceManagement.Domain.Schedule", null)
                        .WithMany("ScheduleItems")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
