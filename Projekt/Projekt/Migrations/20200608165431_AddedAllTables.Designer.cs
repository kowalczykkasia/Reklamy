﻿// <auto-generated />
using System;
using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdvertApi.Migrations
{
    [DbContext(typeof(CodeFirstContext))]
    [Migration("20200608165431_AddedAllTables")]
    partial class AddedAllTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvertApi.Models.Banner", b =>
                {
                    b.Property<int>("IdAdvertisement")
                        .HasColumnType("int");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<int>("IdCampaign")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("IdAdvertisement")
                        .HasName("Banner_PK");

                    b.HasIndex("IdCampaign");

                    b.ToTable("Banner");
                });

            modelBuilder.Entity("AdvertApi.Models.Building", b =>
                {
                    b.Property<int>("IdBuilding")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("StreetNumber")
                        .HasColumnType("int");

                    b.HasKey("IdBuilding")
                        .HasName("Building_PK");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("AdvertApi.Models.Campaign", b =>
                {
                    b.Property<int>("IdCampaign")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FromIdBuilding")
                        .HasColumnType("int");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<double>("PricePerSquareMeter")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToIdBuilding")
                        .HasColumnType("int");

                    b.HasKey("IdCampaign")
                        .HasName("Campaign_PK");

                    b.HasIndex("FromIdBuilding");

                    b.HasIndex("IdClient");

                    b.HasIndex("ToIdBuilding");

                    b.ToTable("Campaign");
                });

            modelBuilder.Entity("AdvertApi.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IdClient")
                        .HasName("Client_PK");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("AdvertApi.Models.Banner", b =>
                {
                    b.HasOne("AdvertApi.Models.Campaign", "Campaign")
                        .WithMany("Banners")
                        .HasForeignKey("IdCampaign")
                        .HasConstraintName("Banner_Campaign_FK")
                        .IsRequired();
                });

            modelBuilder.Entity("AdvertApi.Models.Campaign", b =>
                {
                    b.HasOne("AdvertApi.Models.Building", "BuildingFrom")
                        .WithMany("CampaignsFrom")
                        .HasForeignKey("FromIdBuilding")
                        .HasConstraintName("BuildingFrom_Campaign_FK")
                        .IsRequired();

                    b.HasOne("AdvertApi.Models.Client", "Client")
                        .WithMany("Campaigns")
                        .HasForeignKey("IdClient")
                        .HasConstraintName("Client_Campaign_FK")
                        .IsRequired();

                    b.HasOne("AdvertApi.Models.Building", "BuildingTo")
                        .WithMany("CampaignsTo")
                        .HasForeignKey("ToIdBuilding")
                        .HasConstraintName("BuildingTo_Campaign_FK")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
