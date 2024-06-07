﻿// <auto-generated />
using System;
using Akademik.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Akademik.Infrastructure.Migrations
{
    [DbContext(typeof(AkademikDbContext))]
    partial class AkademikDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Akademik.Domain.Entities.Resident", b =>
                {
                    b.Property<string>("PESEL")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EncodedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResidentDetailsId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomNumberId")
                        .HasColumnType("int");

                    b.HasKey("PESEL");

                    b.HasIndex("ResidentDetailsId");

                    b.HasIndex("RoomNumberId");

                    b.ToTable("Residents");
                });

            modelBuilder.Entity("Akademik.Domain.Entities.ResidentDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PhotoData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ResidentsDetails");
                });

            modelBuilder.Entity("Akademik.Domain.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfBeds")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Akademik.Domain.Entities.Resident", b =>
                {
                    b.HasOne("Akademik.Domain.Entities.ResidentDetails", "ResidentDetails")
                        .WithMany()
                        .HasForeignKey("ResidentDetailsId");

                    b.HasOne("Akademik.Domain.Entities.Room", "RoomNumber")
                        .WithMany()
                        .HasForeignKey("RoomNumberId");

                    b.Navigation("ResidentDetails");

                    b.Navigation("RoomNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
