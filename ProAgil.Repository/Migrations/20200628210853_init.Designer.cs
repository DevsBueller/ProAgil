﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProAgil.Repository;

namespace ProAgil.Repository.Migrations
{
    [DbContext(typeof(ProAgilContext))]
    [Migration("20200628210853_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProAgil.Domain.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<DateTime>("EventDate");

                    b.Property<string>("ImagemUrl");

                    b.Property<string>("Local");

                    b.Property<int>("QtPeoples");

                    b.Property<string>("Tel");

                    b.Property<string>("Theme");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ProAgil.Domain.Lot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BeginDate");

                    b.Property<int>("EventId");

                    b.Property<DateTime?>("FinalDate");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("ProAgil.Domain.SocialNetwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EventId");

                    b.Property<string>("Name");

                    b.Property<int?>("SpeakerId");

                    b.Property<string>("URL");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SocialNetworks");
                });

            modelBuilder.Entity("ProAgil.Domain.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("MiniCurriculum");

                    b.Property<string>("Name");

                    b.Property<string>("Tel");

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("ProAgil.Domain.SpeakerEvent", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<int>("SpeakerId");

                    b.HasKey("EventId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SpeakerEvents");
                });

            modelBuilder.Entity("ProAgil.Domain.Lot", b =>
                {
                    b.HasOne("ProAgil.Domain.Event", "Event")
                        .WithMany("Lots")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProAgil.Domain.SocialNetwork", b =>
                {
                    b.HasOne("ProAgil.Domain.Event", "Event")
                        .WithMany("SocialNetworks")
                        .HasForeignKey("EventId");

                    b.HasOne("ProAgil.Domain.Speaker", "Speaker")
                        .WithMany("SocialNetwork")
                        .HasForeignKey("SpeakerId");
                });

            modelBuilder.Entity("ProAgil.Domain.SpeakerEvent", b =>
                {
                    b.HasOne("ProAgil.Domain.Event", "Event")
                        .WithMany("SpeakersEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProAgil.Domain.Speaker", "Speaker")
                        .WithMany("SpeakersEvents")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
