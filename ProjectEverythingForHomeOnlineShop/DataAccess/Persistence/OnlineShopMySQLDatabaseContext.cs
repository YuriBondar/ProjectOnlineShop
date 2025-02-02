using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ProjectEverythingForHomeOnlineShop.Application.Services.Implementation;
using ProjectEverythingForHomeOnlineShop.Core.Models;
using ProjectEverythingForHomeOnlineShop.DataAccess.Persistence.Identity;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectEverythingForHomeOnlineShop.DataAccess.Persistence
{
    public class OnlineShopMySQLDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ShopOrder> ShopOrders { get; set; }

        public DbSet<ShopOrderProduct> ShopOrderProductTable { get; set; }

        public DbSet<ShopOrderStatus> ShopOrderStatuses { get; set; }



        private readonly IConfiguration _configuration = null!;

        ILogger<AuthService> _logger;

        public OnlineShopMySQLDatabaseContext(DbContextOptions<OnlineShopMySQLDatabaseContext> options,
                                              IConfiguration configuration,
                                              ILogger<AuthService> logger) : base(options)

        {
            _configuration = configuration;
            _logger = logger;

        }

        /// <summary>
        /// ser up advanced properties and connestions in database
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            try
            {
                var connectionString = _configuration.GetConnectionString("EverythingForHomeOnlineShopDB");
                optionsBuilder.UseMySql(connectionString, 
                                        new MySqlServerVersion(new Version(8, 0, 38)),
                                        mySqlOptions => mySqlOptions.MigrationsAssembly("ProjectEverythingForHomeOnlineShop"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error configuring DbContext:\n {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// setting up database tables properties and relationthips
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerID);
                entity.Property(e => e.CustomerID)
                      .ValueGeneratedOnAdd();
                entity.HasIndex(e => e.CustomerEmail)
                      .IsUnique();
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductID);
                entity.Property(e => e.ProductID)
                      .ValueGeneratedOnAdd();
                entity.HasIndex(e => e.ProductSCU)
                      .IsUnique();
            });

            modelBuilder.Entity<ShopOrder>(entity =>
            {
                entity.HasKey(e => e.ShopOrderID);
                entity.Property(e => e.ShopOrderID)
                      .ValueGeneratedOnAdd();
            });

            /// set up one-to-many bettween ShopOrder and Customer through CustomerID
            modelBuilder.Entity<ShopOrder>()
                       .HasOne(shopOrder => shopOrder.Customer)
                       .WithMany(customer => customer.ShopOrders)
                       .HasForeignKey(shopOrder => shopOrder.CustomerID)
                       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShopOrderProduct>()
                         .HasKey(shopOrderProduct => new
                         {
                             shopOrderProduct.ShopOrderID,
                             shopOrderProduct.ProductID
                         });

            /// set up one-to-many bettween ShopOrderProduct and ShopOrder through ShopOrderID
            modelBuilder.Entity<ShopOrderProduct>()
                        .HasOne(shopOrderProduct => shopOrderProduct.ShopOrder)
                        .WithMany(shopOrder => shopOrder.ShopOrderProducts)
                        .HasForeignKey(shopOrderProduct => shopOrderProduct.ShopOrderID);

            /// set up one-to-many bettween ShopOrderProduct and Priduct through ShopOrderID
            modelBuilder.Entity<ShopOrderProduct>()
                        .HasOne(shopOrderProduct => shopOrderProduct.Product)
                        .WithMany(product => product.ShopOrderProducts)
                        .HasForeignKey(shopOrderProduct => shopOrderProduct.ProductID);

            modelBuilder.Entity<ShopOrder>()
                      .HasOne(shopOrder => shopOrder.ShopOrderStatus)
                      .WithMany(shopOrderStatus => shopOrderStatus.ShopOrders)
                      .HasForeignKey(shopOrder => shopOrder.ShopOrderStatusID);

            /// seed values for OrderStatus table
            
            modelBuilder.Entity<ShopOrderStatus>().HasData(
                        new ShopOrderStatus { ShopOrderStatusID = 1, ShopOrderStatusName = "Accepted" },
                        new ShopOrderStatus { ShopOrderStatusID = 2, ShopOrderStatusName = "Shipped" },
                        new ShopOrderStatus { ShopOrderStatusID = 3, ShopOrderStatusName = "Completted" },
                        new ShopOrderStatus { ShopOrderStatusID = 100, ShopOrderStatusName = "Canceled" }
            );


        }

    }
}
