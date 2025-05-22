using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AdvancedAjax.Models;

namespace AdvancedAjax.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}