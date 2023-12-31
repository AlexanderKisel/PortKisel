﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortKisel.Context;

#nullable disable

namespace PortKisel.Context.Migrations
{
    [DbContext(typeof(PortContext))]
    partial class PortContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Cargo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyZakazchikId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyZakazchikId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Cargo_Name")
                        .HasFilter("DeletedAt is null");

                    b.HasIndex("Weight")
                        .IsUnique()
                        .HasDatabaseName("IX_Cargo_Weight")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("Cargo", (string)null);
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.CompanyPer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_CompanyPer_Name")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("CompanyPer", (string)null);
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.CompanyZakazchik", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_CompanyPer_Name")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("CompanyZakazchik", (string)null);
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Documenti", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CargoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("IssaedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("VesselId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("StaffId");

                    b.HasIndex("VesselId");

                    b.ToTable("Documenti", (string)null);
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Post")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Staff", (string)null);
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Vessel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyPerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoadCapacity")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyPerId");

                    b.HasIndex("LoadCapacity")
                        .IsUnique()
                        .HasDatabaseName("IX_Cargo_Weight")
                        .HasFilter("DeletedAt is null");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Cargo_Name")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("Vessel", (string)null);
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Cargo", b =>
                {
                    b.HasOne("PortKisel.Context.Contracts.Models.CompanyZakazchik", "CompanyZakazchik")
                        .WithMany("Cargo")
                        .HasForeignKey("CompanyZakazchikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyZakazchik");
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Documenti", b =>
                {
                    b.HasOne("PortKisel.Context.Contracts.Models.Cargo", "Cargo")
                        .WithMany("Documenti")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortKisel.Context.Contracts.Models.Staff", "Staff")
                        .WithMany("Documenti")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortKisel.Context.Contracts.Models.Vessel", "Vessel")
                        .WithMany("Documenti")
                        .HasForeignKey("VesselId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Staff");

                    b.Navigation("Vessel");
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Vessel", b =>
                {
                    b.HasOne("PortKisel.Context.Contracts.Models.CompanyPer", "CompanyPer")
                        .WithMany("Vessels")
                        .HasForeignKey("CompanyPerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyPer");
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Cargo", b =>
                {
                    b.Navigation("Documenti");
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.CompanyPer", b =>
                {
                    b.Navigation("Vessels");
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.CompanyZakazchik", b =>
                {
                    b.Navigation("Cargo");
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Staff", b =>
                {
                    b.Navigation("Documenti");
                });

            modelBuilder.Entity("PortKisel.Context.Contracts.Models.Vessel", b =>
                {
                    b.Navigation("Documenti");
                });
#pragma warning restore 612, 618
        }
    }
}
