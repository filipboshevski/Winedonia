﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WineriesApp.DataContext;

#nullable disable

namespace WineriesApp.DataContext.Migrations
{
    [DbContext(typeof(WineriesDbContext))]
    [Migration("20231216172910_Populate_With_Initial_Winery_Data")]
    partial class Populate_With_Initial_Winery_Data
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Wine_Winery", b =>
                {
                    b.Property<Guid>("WineryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WineId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WineryId", "WineId");

                    b.HasIndex("WineId");

                    b.ToTable("Wine_Winery");
                });

            modelBuilder.Entity("WineriesApp.DataContext.Models.Municipality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Municipality");
                });

            modelBuilder.Entity("WineriesApp.DataContext.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("WineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WineryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WineId");

                    b.HasIndex("WineryId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("WineriesApp.DataContext.Models.Wine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Wine");
                });

            modelBuilder.Entity("WineriesApp.DataContext.Models.Winery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.Property<Guid?>("MunicipalityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.ToTable("Winery");
                });

            modelBuilder.Entity("Wine_Winery", b =>
                {
                    b.HasOne("WineriesApp.DataContext.Models.Wine", null)
                        .WithMany()
                        .HasForeignKey("WineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WineriesApp.DataContext.Models.Winery", null)
                        .WithMany()
                        .HasForeignKey("WineryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WineriesApp.DataContext.Models.Review", b =>
                {
                    b.HasOne("WineriesApp.DataContext.Models.Wine", "Wine")
                        .WithMany()
                        .HasForeignKey("WineId");

                    b.HasOne("WineriesApp.DataContext.Models.Winery", "Winery")
                        .WithMany()
                        .HasForeignKey("WineryId");

                    b.Navigation("Wine");

                    b.Navigation("Winery");
                });

            modelBuilder.Entity("WineriesApp.DataContext.Models.Winery", b =>
                {
                    b.HasOne("WineriesApp.DataContext.Models.Municipality", "Municipality")
                        .WithMany("Wineries")
                        .HasForeignKey("MunicipalityId");

                    b.Navigation("Municipality");
                });

            modelBuilder.Entity("WineriesApp.DataContext.Models.Municipality", b =>
                {
                    b.Navigation("Wineries");
                });
#pragma warning restore 612, 618
        }
    }
}
