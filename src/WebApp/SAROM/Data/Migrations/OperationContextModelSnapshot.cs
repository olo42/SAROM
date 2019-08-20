﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SAROM.Models;

namespace SAROM.Migrations
{
    [DbContext(typeof(OperationContext))]
    partial class OperationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SAROM.Models.MissingPerson", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ailments");

                    b.Property<string>("Clothes");

                    b.Property<string>("DateOfBirth");

                    b.Property<string>("EyesColour");

                    b.Property<string>("FurtherInformation");

                    b.Property<string>("Gender");

                    b.Property<string>("HairColor");

                    b.Property<string>("KnownPlaces");

                    b.Property<string>("Medications");

                    b.Property<DateTime>("MissingSince");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OperationId");

                    b.Property<string>("Size");

                    b.Property<string>("SkinType");

                    b.Property<string>("SpecialCharacteristics");

                    b.Property<string>("Weight");

                    b.HasKey("Id");

                    b.ToTable("MissingPerson");
                });

            modelBuilder.Entity("SAROM.Models.Operation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlertDate");

                    b.Property<string>("AlertTime");

                    b.Property<string>("ClosingReport");

                    b.Property<string>("Headquarter");

                    b.Property<string>("HeadquarterContact");

                    b.Property<bool>("IsClosed");

                    b.Property<string>("Name");

                    b.Property<string>("Number");

                    b.Property<string>("PoliceContact");

                    b.Property<string>("PoliceContactPhone");

                    b.HasKey("Id");

                    b.ToTable("Operation");
                });

            modelBuilder.Entity("SAROM.Models.OperationAction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Message");

                    b.Property<string>("OperationId")
                        .IsRequired();

                    b.Property<string>("UnitName");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.ToTable("OperationAction");
                });

            modelBuilder.Entity("SAROM.Models.Person", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Dog");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.Property<string>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("SAROM.Models.Unit", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaSeeker");

                    b.Property<int>("DebrisSearcher");

                    b.Property<string>("GroupLeader");

                    b.Property<int>("Helpers");

                    b.Property<int>("Mantrailer");

                    b.Property<string>("Name");

                    b.Property<string>("OperationId");

                    b.Property<string>("PagerNumber");

                    b.Property<int>("WaterLocators");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("SAROM.Models.OperationAction", b =>
                {
                    b.HasOne("SAROM.Models.Operation")
                        .WithMany("OperationActions")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SAROM.Models.Person", b =>
                {
                    b.HasOne("SAROM.Models.Unit")
                        .WithMany("People")
                        .HasForeignKey("UnitId");
                });

            modelBuilder.Entity("SAROM.Models.Unit", b =>
                {
                    b.HasOne("SAROM.Models.Operation")
                        .WithMany("Units")
                        .HasForeignKey("OperationId");
                });
#pragma warning restore 612, 618
        }
    }
}
