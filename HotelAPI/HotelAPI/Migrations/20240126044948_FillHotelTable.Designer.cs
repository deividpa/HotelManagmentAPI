﻿// <auto-generated />
using System;
using HotelAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240126044948_FillHotelTable")]
    partial class FillHotelTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelAPI.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 100,
                            City = "Medellin",
                            CreationDate = new DateTime(2024, 1, 25, 23, 49, 48, 861, DateTimeKind.Local).AddTicks(271),
                            Detail = "Detail Test",
                            ImageURL = "",
                            Name = "Test",
                            Price = 10000.5,
                            UpdateDate = new DateTime(2024, 1, 25, 23, 49, 48, 861, DateTimeKind.Local).AddTicks(282)
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 200,
                            City = "Bogotá",
                            CreationDate = new DateTime(2024, 1, 25, 23, 49, 48, 861, DateTimeKind.Local).AddTicks(285),
                            Detail = "Detail Test",
                            ImageURL = "",
                            Name = "Test 2",
                            Price = 20000.5,
                            UpdateDate = new DateTime(2024, 1, 25, 23, 49, 48, 861, DateTimeKind.Local).AddTicks(286)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}