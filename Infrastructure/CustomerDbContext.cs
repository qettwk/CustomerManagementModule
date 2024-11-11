using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure
{
    public class CustomerDbContext: DbContext
    {
        protected readonly IConfiguration configuration;

        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("AppDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .ToTable("Customers", "CustomerManagment");
        }
    }
}
