﻿// <auto-generated />
using System;
using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeeShop.DataAccess.Migrations
{
    [DbContext(typeof(CoffeeShopContext))]
    [Migration("20240116175740_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrderId")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrices")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsStock")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<Guid>("PromotionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PromotionId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel.Promotion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePromotion")
                        .HasColumnType("int");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdUserNewsLetter")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("IdUserNewsLetter")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels.UserWithNewsLetter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsNewsLetterActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Newsletters");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels.Order", b =>
                {
                    b.HasOne("CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel.Product", b =>
                {
                    b.HasOne("CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel.Promotion", "Promotion")
                        .WithMany()
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels.User", b =>
                {
                    b.HasOne("CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels.UserWithNewsLetter", "UserWithNewsLetter")
                        .WithOne("User")
                        .HasForeignKey("CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels.User", "IdUserNewsLetter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserWithNewsLetter");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels.User", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels.UserWithNewsLetter", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
