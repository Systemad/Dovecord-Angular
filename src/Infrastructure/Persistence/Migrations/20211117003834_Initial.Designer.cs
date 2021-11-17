﻿// <auto-generated />
using System;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(DoveDbContext))]
    [Migration("20211117003834_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Domain.Entities.ChannelMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEdit")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TextChannelId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TextChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("ChannelMessages");

                    b.HasData(
                        new
                        {
                            Id = new Guid("72ff6abf-ebc0-4f7c-85d5-0ede201da17b"),
                            Content = "First ever channel message",
                            CreatedAt = new DateTime(2021, 11, 17, 1, 38, 34, 373, DateTimeKind.Local).AddTicks(4950),
                            IsEdit = false,
                            TextChannelId = new Guid("f81cf04e-d204-4493-aaa9-76121ab95291"),
                            UserId = new Guid("ca0f4479-5992-4a00-a3d5-d73ae1daff6f"),
                            Username = "danova"
                        });
                });

            modelBuilder.Entity("Domain.Entities.TextChannel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TextChannels");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f81cf04e-d204-4493-aaa9-76121ab95291"),
                            Name = "General"
                        },
                        new
                        {
                            Id = new Guid("9c73037b-5d64-4c56-bf8f-1dea5c4aadf8"),
                            Name = "Random"
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Online")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ca0f4479-5992-4a00-a3d5-d73ae1daff6f"),
                            Online = false,
                            Username = "danova"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ChannelMessage", b =>
                {
                    b.HasOne("Domain.Entities.TextChannel", "TextChannel")
                        .WithMany("ChannelMessages")
                        .HasForeignKey("TextChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TextChannel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany("Users")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Domain.Entities.TextChannel", b =>
                {
                    b.Navigation("ChannelMessages");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
