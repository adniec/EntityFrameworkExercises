using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
        public ICollection<Product> Products { get; set; }
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
                Product product = new Product { ProductName = "Paper" };
                context.Products.Add( product );
                context.Products.Add(new Product { ProductName = "Rock" });
                context.Products.Add(new Product { ProductName = "Scissors" });

                Supplier supplier = new Supplier { CompanyName = "YourShop", Street = "Flawless", City = "New York" };
                context.Suppliers.Add(supplier);
                context.SaveChanges();

                var query = (from p in context.Products where p.ProductName != "Paper" select p).ToList();
                supplier.Products = query;
                product.Supplier = supplier;
                context.SaveChanges();
            }
        }
    }
}
