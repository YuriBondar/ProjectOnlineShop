﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence;

#nullable disable

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Migrations
{
    [DbContext(typeof(OnlineShopMySQLDatabaseContext))]
    [Migration("20250131130734_correctTypeOFUnitPriceForProduct2")]
    partial class correctTypeOFUnitPriceForProduct2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("CustomerCity")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CustomerFirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CustomerHausNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("CustomerLastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("CustomerPostIndex")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("CustomerStreet")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("IdentityUserID")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("CustomerID");

                    b.HasIndex("CustomerEmail")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Category");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProductSCU")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("StockQuantity")
                        .HasMaxLength(30)
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasMaxLength(30)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductID");

                    b.HasIndex("ProductSCU")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrder", b =>
                {
                    b.Property<int>("ShopOrderID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ShopOrderID"));

                    b.Property<int>("CustomerID")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<DateTime>("ShopOrderAcceptedAt")
                        .HasMaxLength(50)
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ShopOrderCompletedAt")
                        .HasMaxLength(30)
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ShopOrderShippedAt")
                        .HasMaxLength(30)
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ShopOrderStatusID")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.HasKey("ShopOrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("ShopOrderStatusID");

                    b.ToTable("ShopOrders");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrderProduct", b =>
                {
                    b.Property<int>("ShopOrderID")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int>("ProductOrderQuantity")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.HasKey("ShopOrderID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("ShopOrderProductTable");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrderStatus", b =>
                {
                    b.Property<int>("ShopOrderStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ShopOrderStatusID"));

                    b.Property<string>("ShopOrderStatusName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ShopOrderStatusID");

                    b.ToTable("ShopOrderStatuses");

                    b.HasData(
                        new
                        {
                            ShopOrderStatusID = 1,
                            ShopOrderStatusName = "Accepted"
                        },
                        new
                        {
                            ShopOrderStatusID = 2,
                            ShopOrderStatusName = "Shipped"
                        },
                        new
                        {
                            ShopOrderStatusID = 3,
                            ShopOrderStatusName = "Completted"
                        },
                        new
                        {
                            ShopOrderStatusID = 100,
                            ShopOrderStatusName = "Canceled"
                        });
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrder", b =>
                {
                    b.HasOne("ProjectEverythingForHomeOnlineShop.Core.Models.Customer", "Customer")
                        .WithMany("ShopOrders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrderStatus", "ShopOrderStatus")
                        .WithMany("ShopOrders")
                        .HasForeignKey("ShopOrderStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("ShopOrderStatus");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrderProduct", b =>
                {
                    b.HasOne("ProjectEverythingForHomeOnlineShop.Core.Models.Product", "Product")
                        .WithMany("ShopOrderProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrder", "ShopOrder")
                        .WithMany("ShopOrderProducts")
                        .HasForeignKey("ShopOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShopOrder");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.Customer", b =>
                {
                    b.Navigation("ShopOrders");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.Product", b =>
                {
                    b.Navigation("ShopOrderProducts");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrder", b =>
                {
                    b.Navigation("ShopOrderProducts");
                });

            modelBuilder.Entity("ProjectEverythingForHomeOnlineShop.Core.Models.ShopOrderStatus", b =>
                {
                    b.Navigation("ShopOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
