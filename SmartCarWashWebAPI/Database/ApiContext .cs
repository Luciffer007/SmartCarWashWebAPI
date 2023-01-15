using Microsoft.EntityFrameworkCore;
using SmartCarWashWebAPI.Models;
using System;

namespace SmartCarWashWebAPI.Database
{
    public class ApiContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Sale> Sales { get; set; }


        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buyer>(b =>
            {
                b.HasKey(b => b.Id);
                b.Property(b => b.Id).ValueGeneratedOnAdd();
                b.HasMany(b => b.Sales).WithOne(s => s.Buyer).HasForeignKey(s => s.BuyerId);
                b.HasData(new Buyer[]
                {
                    new Buyer { Id = 1, Name = "Sergei Likhachev" },
                    new Buyer { Id = 2, Name = "Artyom Gerasimov" },
                    new Buyer { Id = 3, Name = "Alex Lector" },
                    new Buyer { Id = 4, Name = "Max Karcev" }
                });
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Id).ValueGeneratedOnAdd();
                p.HasData(new Product[]
                {
                    new Product { Id = 1, Name = "Sponge", Price = 100.5f },
                    new Product { Id = 2, Name = "Soap", Price = 40.9f },
                    new Product { Id = 3, Name = "Air freshener", Price = 200f },
                    new Product { Id = 4, Name = "Brush", Price = 250f }
                });
            });

            modelBuilder.Entity<Sale>(s =>
            {
                s.HasKey(s => s.Id);
                s.Property(s => s.Id).ValueGeneratedOnAdd();
                s.HasMany(s => s.SalesData).WithOne(sd => sd.Sale).HasForeignKey(sd => sd.SaleId);
                s.HasData(new Sale[]
                {
                    new Sale { Id = 1, DateTime = DateTime.Now, SalesPointId = 1, BuyerId = 1 },
                    new Sale { Id = 2, DateTime = DateTime.Now, SalesPointId = 1, BuyerId = null },
                    new Sale { Id = 3, DateTime = DateTime.Now, SalesPointId = 3, BuyerId = 2 },
                    new Sale { Id = 4, DateTime = DateTime.Now, SalesPointId = 3, BuyerId = 2 },
                    new Sale { Id = 5, DateTime = DateTime.Now, SalesPointId = 1, BuyerId = 1 },
                    new Sale { Id = 6, DateTime = DateTime.Now, SalesPointId = 1, BuyerId = null },
                    new Sale { Id = 7, DateTime = DateTime.Now, SalesPointId = 4, BuyerId = 2 },
                    new Sale { Id = 8, DateTime = DateTime.Now, SalesPointId = 4, BuyerId = 3 }
                });
            });

            modelBuilder.Entity<SaleData>(sd =>
            {
                sd.HasKey(sd => new { sd.ProductId, sd.SaleId });
                sd.HasData(new SaleData[]
                {
                    new SaleData { SaleId = 1, ProductId = 1, ProductQuantity = 13, ProductIdAmount = 1000f },
                    new SaleData { SaleId = 1, ProductId = 4, ProductQuantity = 5, ProductIdAmount = 540f },
                    new SaleData { SaleId = 2, ProductId = 2, ProductQuantity = 3, ProductIdAmount = 300f },
                    new SaleData { SaleId = 3, ProductId = 1, ProductQuantity = 4, ProductIdAmount = 440f },
                    new SaleData { SaleId = 3, ProductId = 3, ProductQuantity = 5, ProductIdAmount = 540f },
                    new SaleData { SaleId = 4, ProductId = 1, ProductQuantity = 6, ProductIdAmount = 600f },
                    new SaleData { SaleId = 5, ProductId = 2, ProductQuantity = 8, ProductIdAmount = 670f },
                    new SaleData { SaleId = 5, ProductId = 4, ProductQuantity = 9, ProductIdAmount = 1100f },
                    new SaleData { SaleId = 6, ProductId = 1, ProductQuantity = 3, ProductIdAmount = 250f },
                    new SaleData { SaleId = 7, ProductId = 3, ProductQuantity = 11, ProductIdAmount = 1000f },
                    new SaleData { SaleId = 7, ProductId = 4, ProductQuantity = 4, ProductIdAmount = 570f },
                    new SaleData { SaleId = 8, ProductId = 1, ProductQuantity = 5, ProductIdAmount = 400f },
                    new SaleData { SaleId = 8, ProductId = 2, ProductQuantity = 7, ProductIdAmount = 750f }
                });
            });

            modelBuilder.Entity<SalesPoint>(sp =>
            {
                sp.HasKey(p => p.Id);
                sp.Property(p => p.Id).ValueGeneratedOnAdd();
                sp.HasMany(sp => sp.ProvidedProducts).WithOne(pp => pp.SalesPoint).HasForeignKey(pp => pp.SalesPointId);
                sp.HasData(new SalesPoint[]
                {
                    new SalesPoint { Id = 1, Name = "Shop 1" },
                    new SalesPoint { Id = 2, Name = "Shop 2" },
                    new SalesPoint { Id = 3, Name = "Shop 3" },
                    new SalesPoint { Id = 4, Name = "Shop 4" }
                });
            });

            modelBuilder.Entity<ProvidedProduct>(pp =>
            {
                pp.HasKey(pp => new { pp.ProductId, pp.SalesPointId });
                pp.HasData(new ProvidedProduct[]
                {
                    new ProvidedProduct { ProductId = 1, SalesPointId = 1, ProductQuantity = 13 },
                    new ProvidedProduct { ProductId = 4, SalesPointId = 1, ProductQuantity = 25 },
                    new ProvidedProduct { ProductId = 2, SalesPointId = 2, ProductQuantity = 7 },
                    new ProvidedProduct { ProductId = 3, SalesPointId = 2, ProductQuantity = 13 },
                    new ProvidedProduct { ProductId = 1, SalesPointId = 3, ProductQuantity = 55 },
                    new ProvidedProduct { ProductId = 3, SalesPointId = 4, ProductQuantity = 23 },
                    new ProvidedProduct { ProductId = 4, SalesPointId = 4, ProductQuantity = 25 }
                });
            });
        }
    }
}
