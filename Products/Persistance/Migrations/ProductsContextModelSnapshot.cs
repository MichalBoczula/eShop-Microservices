﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Products.Persistance.Context;

#nullable disable

namespace Products.Persistance.Migrations
{
    [DbContext(typeof(ProductsContext))]
    partial class ProductsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Products.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImgName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IntegrationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IntegrationId")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImgName = "Huawei",
                            IntegrationId = new Guid("55ccee28-e15d-4644-a7be-2f8a93568d6f"),
                            Name = "Chinese",
                            Price = 2000
                        },
                        new
                        {
                            Id = 2,
                            ImgName = "Samsung",
                            IntegrationId = new Guid("0ef1268e-33d6-49cd-a4b5-8eb94494d896"),
                            Name = "Samsung",
                            Price = 3000
                        },
                        new
                        {
                            Id = 3,
                            ImgName = "Iphone",
                            IntegrationId = new Guid("23363aff-dd71-4f3c-8381-f7e71021761e"),
                            Name = "Iphone",
                            Price = 4000
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
