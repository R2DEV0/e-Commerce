using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        public DbSet<Product> Products {get; set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<Customer> Customers {get; set;}

    }
}