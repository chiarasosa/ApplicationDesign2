﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Obligatorio1.DataAccess.Contexts;

#nullable disable

namespace Obligatorio1.DataAccess.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Obligatorio1.Domain.Cart", b =>
                {
                    b.Property<int>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartID"));

                    b.Property<string>("PromotionApplied")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CartID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Obligatorio1.Domain.CartProduct", b =>
                {
                    b.Property<int>("CartID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("CartID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("CartProduct");
                });

            modelBuilder.Entity("Obligatorio1.Domain.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<bool>("AvailableToPromotions")
                        .HasColumnType("bit");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Obligatorio1.Domain.Purchase", b =>
                {
                    b.Property<int>("PurchaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseID"));

                    b.Property<DateTime?>("DateOfPurchase")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromoApplied")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PurchaseID");

                    b.HasIndex("UserID");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Obligatorio1.Domain.PurchaseProduct", b =>
                {
                    b.Property<int>("PurchaseID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("PurchaseID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("PurchaseProduct");
                });

            modelBuilder.Entity("Obligatorio1.Domain.Session", b =>
                {
                    b.Property<int>("SessionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionID"));

                    b.Property<Guid>("AuthToken")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("SessionID");

                    b.HasIndex("UserID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Obligatorio1.Domain.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Obligatorio1.Domain.Cart", b =>
                {
                    b.HasOne("Obligatorio1.Domain.User", null)
                        .WithOne("Cart")
                        .HasForeignKey("Obligatorio1.Domain.Cart", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Obligatorio1.Domain.CartProduct", b =>
                {
                    b.HasOne("Obligatorio1.Domain.Cart", "Cart")
                        .WithMany("CartProducts")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Obligatorio1.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Obligatorio1.Domain.Purchase", b =>
                {
                    b.HasOne("Obligatorio1.Domain.User", "User")
                        .WithMany("Purchases")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Obligatorio1.Domain.PurchaseProduct", b =>
                {
                    b.HasOne("Obligatorio1.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Obligatorio1.Domain.Purchase", "Purchase")
                        .WithMany("PurchasedProducts")
                        .HasForeignKey("PurchaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("Obligatorio1.Domain.Session", b =>
                {
                    b.HasOne("Obligatorio1.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Obligatorio1.Domain.Cart", b =>
                {
                    b.Navigation("CartProducts");
                });

            modelBuilder.Entity("Obligatorio1.Domain.Purchase", b =>
                {
                    b.Navigation("PurchasedProducts");
                });

            modelBuilder.Entity("Obligatorio1.Domain.User", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}
