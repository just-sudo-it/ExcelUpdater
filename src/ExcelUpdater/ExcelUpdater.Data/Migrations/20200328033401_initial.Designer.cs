﻿// <auto-generated />
using System;
using ExcelUpdater.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExcelUpdater.Data.Migrations
{
    [DbContext(typeof(RecordContext))]
    [Migration("20200328033401_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExceUpdater.Domain.Record", b =>
                {
                    b.Property<string>("My_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("My_Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Time1")
                        .HasColumnType("real");

                    b.Property<float>("Time10")
                        .HasColumnType("real");

                    b.Property<float>("Time11")
                        .HasColumnType("real");

                    b.Property<float>("Time12")
                        .HasColumnType("real");

                    b.Property<float>("Time13")
                        .HasColumnType("real");

                    b.Property<float>("Time14")
                        .HasColumnType("real");

                    b.Property<float>("Time15")
                        .HasColumnType("real");

                    b.Property<float>("Time16")
                        .HasColumnType("real");

                    b.Property<float>("Time17")
                        .HasColumnType("real");

                    b.Property<float>("Time18")
                        .HasColumnType("real");

                    b.Property<float>("Time19")
                        .HasColumnType("real");

                    b.Property<float>("Time2")
                        .HasColumnType("real");

                    b.Property<float>("Time20")
                        .HasColumnType("real");

                    b.Property<float>("Time21")
                        .HasColumnType("real");

                    b.Property<float>("Time22")
                        .HasColumnType("real");

                    b.Property<float>("Time23")
                        .HasColumnType("real");

                    b.Property<float>("Time24")
                        .HasColumnType("real");

                    b.Property<float>("Time3")
                        .HasColumnType("real");

                    b.Property<float>("Time4")
                        .HasColumnType("real");

                    b.Property<float>("Time5")
                        .HasColumnType("real");

                    b.Property<float>("Time6")
                        .HasColumnType("real");

                    b.Property<float>("Time7")
                        .HasColumnType("real");

                    b.Property<float>("Time8")
                        .HasColumnType("real");

                    b.Property<float>("Time9")
                        .HasColumnType("real");

                    b.HasKey("My_Id", "My_Date");

                    b.ToTable("Records");
                });
#pragma warning restore 612, 618
        }
    }
}
