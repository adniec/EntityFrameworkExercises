using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App
{
    public abstract class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
    public class Supplier : Company
    {
        public string BankAccountNumber { get; set; }
    }
    public class Customer : Company
    {
        public int Discount { get; set; }
    }
    public class CompanyContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Supplier>().ToTable("Suppliers");
            builder.Entity<Customer>().ToTable("Customers");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Datasource=CompaniesDatabase.db");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CompanyContext()) {
                context.Companies.Add(new Supplier {
                    CompanyName = "Coffee2Go", Street = "Thirsty Ave.", City = "New York", ZipCode = "10032",
                    BankAccountNumber = "12345678901234567890123456" 
                });
                context.Companies.Add(new Supplier { 
                    CompanyName = "YummyFood", Street = "Hunger Blvd.", City = "New York", ZipCode = "10107",
                    BankAccountNumber = "09876543210987654321098765" 
                });
                context.Companies.Add(new Customer {
                    CompanyName = "BigDeal", Street = "Money St.", City = "New York", ZipCode = "10045", Discount = 10 
                });
                context.Companies.Add(new Customer {
                    CompanyName = "BuyFast", Street = "Cash St.", City = "New York", ZipCode = "10111", Discount = 50
                });
                context.SaveChanges();

                Console.WriteLine("All companies:");
                var allQuery = from c in context.Companies select c.CompanyName;
                foreach (var name in allQuery) Console.WriteLine(name);

                Console.WriteLine("\nCustomers:");
                var cusQuery = context.Companies.OfType<Customer>().Select(d => d.CompanyName);
                foreach (var name in cusQuery) Console.WriteLine(name);

                Console.WriteLine("\nSuppliers:");
                var supQuery = context.Companies.Select(b => b is Supplier ? (b as Supplier).CompanyName : null).ToList();
                foreach (var name in supQuery) Console.WriteLine(name);
            }
        }
    }
}
