﻿// <auto-generated />
using System;
using FastDrink.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FastDrink.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220303224054_Add Contrains In Details")]
    partial class AddContrainsInDetails
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FastDrink.Domain.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.AlcoholDetails", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("AlcoholContent")
                        .HasColumnType("real");

                    b.HasKey("ProductId");

                    b.ToTable("AlcoholDetails");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.BeerDetails", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("AlcoholContent")
                        .HasColumnType("real");

                    b.Property<string>("Style")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductId");

                    b.ToTable("BeerDetails");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Container");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.EnergyDrinkDetails", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<bool>("Dietetics")
                        .HasColumnType("bit");

                    b.Property<bool>("Energizing")
                        .HasColumnType("bit");

                    b.Property<string>("Flavor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("NonAlcoholic")
                        .HasColumnType("bit");

                    b.HasKey("ProductId");

                    b.ToTable("EnergyDrinkDetails");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.FlavorDetails", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Flavor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductId");

                    b.ToTable("FlavorDetails");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("AddressId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ContainerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<float?>("Discount")
                        .HasColumnType("real");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<float>("Volume")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ContainerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.ProductPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("PhotoId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPhoto");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.SodaDetails", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<bool>("Dietetics")
                        .HasColumnType("bit");

                    b.Property<string>("Flavor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductId");

                    b.ToTable("SodaDetails");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.WaterDetails", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<bool>("Gasified")
                        .HasColumnType("bit");

                    b.Property<bool>("LowInSodium")
                        .HasColumnType("bit");

                    b.HasKey("ProductId");

                    b.ToTable("WaterDetails");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.WineDetails", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("AlcoholContent")
                        .HasColumnType("real");

                    b.Property<string>("Style")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Variety")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductId");

                    b.ToTable("WineDetails");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.AlcoholDetails", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Product", null)
                        .WithOne("AlcoholDetails")
                        .HasForeignKey("FastDrink.Domain.Entities.AlcoholDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.BeerDetails", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Product", null)
                        .WithOne("BeerDetails")
                        .HasForeignKey("FastDrink.Domain.Entities.BeerDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.EnergyDrinkDetails", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Product", null)
                        .WithOne("EnergyDrinkDetails")
                        .HasForeignKey("FastDrink.Domain.Entities.EnergyDrinkDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.FlavorDetails", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Product", null)
                        .WithOne("FlavorDetails")
                        .HasForeignKey("FastDrink.Domain.Entities.FlavorDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Order", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FastDrink.Domain.Entities.Order", null)
                        .WithMany("Orders")
                        .HasForeignKey("OrderId");

                    b.HasOne("FastDrink.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.OrderProduct", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Address", null)
                        .WithMany("Addresses")
                        .HasForeignKey("AddressId");

                    b.HasOne("FastDrink.Domain.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FastDrink.Domain.Entities.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Product", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FastDrink.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FastDrink.Domain.Entities.Container", "Container")
                        .WithMany("Products")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Container");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.ProductPhoto", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Product", "Product")
                        .WithMany("Photos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.SodaDetails", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Product", null)
                        .WithOne("SodaDetails")
                        .HasForeignKey("FastDrink.Domain.Entities.SodaDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.User", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Address", "Address")
                        .WithOne("User")
                        .HasForeignKey("FastDrink.Domain.Entities.User", "AddressId");

                    b.HasOne("FastDrink.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.WaterDetails", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Product", null)
                        .WithOne("WaterDetails")
                        .HasForeignKey("FastDrink.Domain.Entities.WaterDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.WineDetails", b =>
                {
                    b.HasOne("FastDrink.Domain.Entities.Product", null)
                        .WithOne("WineDetails")
                        .HasForeignKey("FastDrink.Domain.Entities.WineDetails", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Address", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Container", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Order", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Product", b =>
                {
                    b.Navigation("AlcoholDetails");

                    b.Navigation("BeerDetails");

                    b.Navigation("EnergyDrinkDetails");

                    b.Navigation("FlavorDetails");

                    b.Navigation("Orders");

                    b.Navigation("Photos");

                    b.Navigation("SodaDetails");

                    b.Navigation("WaterDetails");

                    b.Navigation("WineDetails");
                });

            modelBuilder.Entity("FastDrink.Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
