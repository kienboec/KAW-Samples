using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HelloSQLite
{
    class DatabaseContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=testdb.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.OrderId);
                entity.Property(x => x.CustomerName);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.ProductId);
                entity.Property(x => x.Name);
                entity.Property(x => x.Price);
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(x => x.ProductOrderId);
                entity.HasOne<Order>(x => x.Order);
                entity.HasOne<Product>(x => x.Product);
            });

            base.OnModelCreating(modelBuilder);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DatabaseContext context =  new DatabaseContext();
            context.Database.EnsureCreated();

            if (context.Products.Count() == 0)
            {
                context.Products.Add(new Product(1, "rice-cheese", 5));
                context.Add<Product>(new Product(2, "Hummus", 7));
                context.Add<Product>(new Product(3, "Mozzarella", 50));
                context.SaveChanges();
            }
            else
            {
                Product rc = context.Products.First(p => p.ProductId == 1);
                rc.Price += 1;
                context.SaveChanges();
            }

            if (context.Orders.Count() == 0)
            {
                context.Orders.Add(new Order() {CustomerName = "Me", OrderId = 1});
                context.SaveChanges();
            }

            ProductOrder productOrder = new ProductOrder()
            {
                ProductOrderId = 1,
                Order = context.Orders.First(x => x.OrderId == 1),
                Product = context.Products.Where(x => x.Name.Contains("rice")).First(),
                Quantity = 1,
            };
            context.Add(productOrder);
            context.SaveChanges();

            Console.WriteLine("everything is awesome...");
        }
    }
}
