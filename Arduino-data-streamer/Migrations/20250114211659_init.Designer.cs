﻿// <auto-generated />
using System;
using Arduino_data_streamer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Arduino_data_streamer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250114211659_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.12");

            modelBuilder.Entity("Arduino_data_streamer.DataModel", b =>
                {
                    b.Property<string>("BotId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOnLine")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MotorsSpeed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SensorFrontDistance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SensorLeftDistance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SensorRightDistance")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("TEXT");

                    b.ToTable("DataModels");
                });
#pragma warning restore 612, 618
        }
    }
}
