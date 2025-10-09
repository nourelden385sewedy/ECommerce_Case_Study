using ECommerce_Case_Study.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce_Case_Study.Data
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product.Name unique
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            // Customer <--> CustomerProfile (1:1)
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CustomerProfile)
                .WithOne(cp => cp.Customer)
                .HasForeignKey<CustomerProfile>(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer <--> Order (1:M)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            /*// Order <--> OrderItem (1:M)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product <--> OrderItem (1:M)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.SetNull);*/

            // Old
            // Product ↔ Order (M:N)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders)
                .UsingEntity(j => j.ToTable("OrderProducts"));



            // Category <--> Product (1:M)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // -------------------- DATA SEEDING --------------------

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Devices and gadgets" },
                new Category { Id = 2, Name = "Books", Description = "Reading and education" },
                new Category { Id = 3, Name = "Clothing", Description = "Apparel and accessories" }
            );

            // Products (3 per category)
            modelBuilder.Entity<Product>().HasData(
                // Electronics
                new Product { Id = 1, Name = "Smartphone", Description = "Latest smartphone", StockQuantity = 50, Price = 899, CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", Description = "High performance laptop", StockQuantity = 30, Price = 1299, CategoryId = 1 },
                new Product { Id = 3, Name = "Headphones", Description = "Noise cancelling", StockQuantity = 80, Price = 199, CategoryId = 1 },

                // Books
                new Product { Id = 4, Name = "C# in Depth", Description = "Programming guide", StockQuantity = 100, Price = 49, CategoryId = 2 },
                new Product { Id = 5, Name = "Clean Code", Description = "Best coding practices", StockQuantity = 75, Price = 59, CategoryId = 2 },
                new Product { Id = 6, Name = "Design Patterns", Description = "Software architecture", StockQuantity = 60, Price = 79, CategoryId = 2 },

                // Clothing
                new Product { Id = 7, Name = "T-Shirt", Description = "Cotton T-shirt", StockQuantity = 200, Price = 19, CategoryId = 3 },
                new Product { Id = 8, Name = "Jeans", Description = "Blue denim jeans", StockQuantity = 150, Price = 49, CategoryId = 3 },
                new Product { Id = 9, Name = "Jacket", Description = "Leather jacket", StockQuantity = 40, Price = 149, CategoryId = 3 }
            );

            // Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Alice Johnson", Contact = "555-1111", Email = "alice@example.com", Password = "Password123!", ConfirmPassword = "Password123!" },
                new Customer { Id = 2, Name = "Bob Smith", Contact = "555-2222", Email = "bob@example.com", Password = "Password123!", ConfirmPassword = "Password123!" },
                new Customer { Id = 3, Name = "Charlie Brown", Contact = "555-3333", Email = "charlie@example.com", Password = "Password123!", ConfirmPassword = "Password123!" },
                new Customer { Id = 4, Name = "Diana Prince", Contact = "555-4444", Email = "diana@example.com", Password = "Password123!", ConfirmPassword = "Password123!" }
            );

            // Customer Profiles (only for first 3 customers)
            modelBuilder.Entity<CustomerProfile>().HasData(
                new CustomerProfile { Id = 1, Address = "123 Main St", DateOfBirth = new DateTime(1990, 5, 10), CustomerId = 1 },
                new CustomerProfile { Id = 2, Address = "456 Elm St", DateOfBirth = new DateTime(1985, 11, 23), CustomerId = 2 },
                new CustomerProfile { Id = 3, Address = "789 Oak St", DateOfBirth = new DateTime(1992, 3, 14), CustomerId = 3 }
            );

            // Orders (only for first 3 customers)
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderId = 1001, TotalPrice = 948, DateOfOrder = new DateTime(2025, 9, 30, 10, 0, 0), CustomerId = 1 },
                new Order { Id = 2, OrderId = 1002, TotalPrice = 59, DateOfOrder = new DateTime(2025, 10, 2, 14, 30, 0), CustomerId = 2 },
                new Order { Id = 3, OrderId = 1003, TotalPrice = 198, DateOfOrder = new DateTime(2025, 10, 6, 9, 15, 0), CustomerId = 3 },
                new Order { Id = 4, OrderId = 1004, TotalPrice = 1299, DateOfOrder = new DateTime(2025, 10, 4, 16, 45, 0), CustomerId = 1 } // Alice has 2 orders
            );

            // OrderItems (explicit junction table)
            /*modelBuilder.Entity<OrderItem>().HasData(
                // Order 1: Smartphone + Headphones
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, PriceAtOrder = 899, Quantity = 1 },
                new OrderItem { Id = 2, OrderId = 1, ProductId = 3, PriceAtOrder = 199, Quantity = 1 },

                // Order 2: Clean Code
                new OrderItem { Id = 3, OrderId = 2, ProductId = 5, PriceAtOrder = 59, Quantity = 1 },

                // Order 3: T-Shirt + Jeans
                new OrderItem { Id = 4, OrderId = 3, ProductId = 7, PriceAtOrder = 19, Quantity = 2 },
                new OrderItem { Id = 5, OrderId = 3, ProductId = 8, PriceAtOrder = 49, Quantity = 2 },

                // Order 4: Laptop
                new OrderItem { Id = 6, OrderId = 4, ProductId = 2, PriceAtOrder = 1299, Quantity = 1 }
            );*/

            // Many-to-Many (Order ↔ Product)
            /*modelBuilder.Entity("OrderProducts").HasData( 
            // Order 1: Smartphone + Headphones
             new { Id = 1, OrdersId = 1, ProductsId = 1 },
            new { Id = 2, OrdersId = 1, ProductsId = 3 }, 
            // Order 2: Clean Code
            new { Id = 3, OrdersId = 2, ProductsId = 5 }, 
            // Order 3: T-Shirt + Jeans
            new { Id = 4, OrdersId = 3, ProductsId = 7 },
            new { Id = 5, OrdersId = 3, ProductsId = 8 }, 
            // Order 4: Laptop
            new { Id = 6, OrdersId = 4, ProductsId = 2 } );*/
        }
    }
}
