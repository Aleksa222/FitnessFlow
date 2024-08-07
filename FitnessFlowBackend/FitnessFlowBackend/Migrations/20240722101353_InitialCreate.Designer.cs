﻿// <auto-generated />
using System;
using FitnessFlowBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitnessFlowBackend.Migrations
{
    [DbContext(typeof(FitnessFlowDbContext))]
    [Migration("20240722101353_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FitnessFlowBackend.Models.Training", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CaloriesBurned")
                        .HasColumnType("integer");

                    b.Property<int>("DurationInSeconds")
                        .HasColumnType("integer");

                    b.Property<int>("Fatigue")
                        .HasColumnType("integer");

                    b.Property<int>("Intensity")
                        .HasColumnType("integer");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TrainingDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TrainingType")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("trainings", (string)null);
                });

            modelBuilder.Entity("FitnessFlowBackend.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("FitnessFlowBackend.Models.Training", b =>
                {
                    b.HasOne("FitnessFlowBackend.Models.User", null)
                        .WithMany("Trainings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FitnessFlowBackend.Models.User", b =>
                {
                    b.Navigation("Trainings");
                });
#pragma warning restore 612, 618
        }
    }
}
