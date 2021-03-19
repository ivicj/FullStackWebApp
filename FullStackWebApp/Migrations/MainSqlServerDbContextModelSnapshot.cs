﻿// <auto-generated />
using System;
using FullStackWebApp.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FullStackWebApp.Migrations
{
    [DbContext(typeof(MainSqlServerDbContext))]
    partial class MainSqlServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FullStackWebApp.Models.Aanbod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MakelaarId")
                        .HasColumnType("int");

                    b.Property<bool>("Tuin")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("MakelaarId");

                    b.ToTable("Aanbod");
                });

            modelBuilder.Entity("FullStackWebApp.Models.Makelaar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Makelaar");
                });

            modelBuilder.Entity("FullStackWebApp.Models.Aanbod", b =>
                {
                    b.HasOne("FullStackWebApp.Models.Makelaar", "Makelaar")
                        .WithMany()
                        .HasForeignKey("MakelaarId");

                    b.Navigation("Makelaar");
                });
#pragma warning restore 612, 618
        }
    }
}
