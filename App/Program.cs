using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace App
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int UnitsOnStock { get; set; }
        public Supplier Supplier { get; set; }
    }
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
    public class ProductContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Datasource=ProductsDatabase.db");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ProductContext())
            {
                Console.WriteLine("Enter product name:");
                string productName = Console.ReadLine();
                context.Products.Add(new Product { ProductName = productName });

                Supplier supplier = new Supplier { CompanyName = "YourShop", Street = "Flawless", City = "New York" };
                context.Suppliers.Add(supplier);
                context.SaveChanges();

                Console.WriteLine("Supplier added to:");
                var query = from p in context.Products where p.ProductName == productName select p;
                foreach (var p in query)
                {
                    p.Supplier = supplier;
                    Console.WriteLine(p.ProductName);
                }
                context.SaveChanges();
            }
        }
    }
}
