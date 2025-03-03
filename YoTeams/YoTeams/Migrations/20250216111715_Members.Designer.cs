﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YoTeams;

#nullable disable

namespace YoTeams.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250216111715_Members")]
    partial class Members
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("YoTeams.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Member 1",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Member 2",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Member 3",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Member 4",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Member 5",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Member 6",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Member 7",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Member 8",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Member 9",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Member 10",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Member 11",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Member 12",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Member 13",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Member 14",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Member 15",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Member 16",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Member 17",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Member 18",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Member 19",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Member 20",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Member 21",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Member 22",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Member 23",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Member 24",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Member 25",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 26,
                            Name = "Member 26",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 27,
                            Name = "Member 27",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 28,
                            Name = "Member 28",
                            Role = "Developer"
                        },
                        new
                        {
                            Id = 29,
                            Name = "Member 29",
                            Role = "Designer"
                        },
                        new
                        {
                            Id = 30,
                            Name = "Member 30",
                            Role = "Developer"
                        });
                });

            modelBuilder.Entity("YoTeams.Models.SocialItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SocialItems");
                });
#pragma warning restore 612, 618
        }
    }
}
