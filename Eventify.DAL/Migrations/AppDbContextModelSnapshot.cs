﻿// <auto-generated />
using System;
using Eventify.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eventify.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Eventify.Domain.DbAttendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AttendeeType")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Attendee");
                });

            modelBuilder.Entity("Eventify.Domain.DbEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Eventify.Domain.DbEventAttendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttendeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DbAttendeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int?>("Participants")
                        .HasColumnType("integer");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AttendeeId");

                    b.HasIndex("DbAttendeeId");

                    b.HasIndex("EventId");

                    b.HasIndex("PaymentMethod");

                    b.ToTable("EventAttendee");
                });

            modelBuilder.Entity("Eventify.Domain.DbPaymentMethod", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("Eventify.Domain.DbEventAttendee", b =>
                {
                    b.HasOne("Eventify.Domain.DbAttendee", "Attendee")
                        .WithMany()
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eventify.Domain.DbAttendee", null)
                        .WithMany("AttendedEvents")
                        .HasForeignKey("DbAttendeeId");

                    b.HasOne("Eventify.Domain.DbEvent", "Event")
                        .WithMany("EventAttendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eventify.Domain.DbPaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("PaymentMethod")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendee");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Eventify.Domain.DbAttendee", b =>
                {
                    b.Navigation("AttendedEvents");
                });

            modelBuilder.Entity("Eventify.Domain.DbEvent", b =>
                {
                    b.Navigation("EventAttendees");
                });
#pragma warning restore 612, 618
        }
    }
}
