﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnitsConverterApp.MVVM.Models.DataModels;

namespace UnitsConverterApp.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20221209111813_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("UnitsConverterApp.MVVM.Models.DataModels.Entities.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Ratio")
                        .HasColumnType("REAL");

                    b.Property<string>("Symbol")
                        .HasColumnType("TEXT");

                    b.Property<int>("UnitTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UnitTypeId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("UnitsConverterApp.MVVM.Models.DataModels.Entities.UnitType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UnitTypeName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UnitsType");
                });

            modelBuilder.Entity("UnitsConverterApp.MVVM.Models.DataModels.Entities.Unit", b =>
                {
                    b.HasOne("UnitsConverterApp.MVVM.Models.DataModels.Entities.UnitType", "UnitType")
                        .WithMany()
                        .HasForeignKey("UnitTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UnitType");
                });
#pragma warning restore 612, 618
        }
    }
}