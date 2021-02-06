using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public class Product
    {
        public Product()
        {
            this.Invoices = new HashSet<Invoice>();
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int UnitsOnStock { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
    public class Invoice
    {
        public Invoice()
        {
            this.Products = new HashSet<Product>();
        }
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class ProductContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

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
                Product paper = new Product { ProductName = "Paper" };
                Product rock = new Product { ProductName = "Rock" };
                Product scissors = new Product { ProductName = "Scissors" };

                context.Products.Add(paper);
                context.Products.Add(rock);
                context.Products.Add(scissors);

                context.Invoices.Add(new Invoice { InvoiceNumber = "198/2020", Products = new List<Product> { paper } });
                context.Invoices.Add(new Invoice { InvoiceNumber = "731/2020", Products = new List<Product> { paper, rock } });
                context.Invoices.Add(new Invoice { InvoiceNumber = "983/2020", Products = new List<Product> { paper, rock, scissors } });

                context.SaveChanges();

                Console.WriteLine("Products sold in transaction 983/2020:");
                var productQuery = from p in context.Products
                                where p.Invoices.Any(i => i.InvoiceNumber == "983/2020")
                                select p;
                foreach (var p in productQuery) Console.WriteLine(p.ProductName);

                Console.WriteLine("\nInvoices containing Paper:");
                var invoiceQuery = from i in context.Invoices
                               where i.Products.Any(p => p.ProductName == "Paper")
                               select i;
                foreach (var i in invoiceQuery) Console.WriteLine(i.InvoiceNumber);
            }
        }
    }
}
