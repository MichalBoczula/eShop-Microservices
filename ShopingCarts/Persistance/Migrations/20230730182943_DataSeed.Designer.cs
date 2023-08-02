﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopingCarts.Persistance.Context;

#nullable disable

namespace ShopingCarts.Persistance.Migrations
{
    [DbContext(typeof(ShoppingCartContext))]
    [Migration("20230730182943_DataSeed")]
    partial class DataSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShopingCarts.Domain.Entities.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("IntegrationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IntegrationId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IntegrationId = new Guid("238d6d50-a1df-4fff-831e-5a919841483e"),
                            Total = 3000,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("ShopingCarts.Domain.Entities.ShoppingCartProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("ProductIntegrationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartProducts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProductIntegrationId = new Guid("0ef1268e-33d6-49cd-a4b5-8eb94494d896"),
                            Quantity = 1,
                            ShoppingCartId = 1,
                            Total = 3000
                        });
                });

            modelBuilder.Entity("ShopingCarts.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("IntegrationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IntegrationId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IntegrationId = new Guid("95464765-cf3f-4ed7-b353-5d2f810dcc33"),
                            ShoppingCartId = 1
                        });
                });

            modelBuilder.Entity("ShopingCarts.Domain.Entities.ShoppingCart", b =>
                {
                    b.HasOne("ShopingCarts.Domain.Entities.User", "UserRef")
                        .WithOne("ShoppingCartRef")
                        .HasForeignKey("ShopingCarts.Domain.Entities.ShoppingCart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRef");
                });

            modelBuilder.Entity("ShopingCarts.Domain.Entities.ShoppingCartProduct", b =>
                {
                    b.HasOne("ShopingCarts.Domain.Entities.ShoppingCart", "ShoppingCartRef")
                        .WithMany("ShoppingCartProducts")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingCartRef");
                });

            modelBuilder.Entity("ShopingCarts.Domain.Entities.ShoppingCart", b =>
                {
                    b.Navigation("ShoppingCartProducts");
                });

            modelBuilder.Entity("ShopingCarts.Domain.Entities.User", b =>
                {
                    b.Navigation("ShoppingCartRef")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}