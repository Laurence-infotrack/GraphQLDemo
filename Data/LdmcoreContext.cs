using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.Data
{
    public class LdmcoreContext : DbContext
    {
        public LdmcoreContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();

            
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasKey(x => x.ServiceId);

            modelBuilder.Entity<Login>()
                .HasKey(x => x.LoginId);

            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.OrderedBy)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.LoginId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);


            modelBuilder.Entity<Order>()
                .HasOne(x => x.Service)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
        }
     }
    
}
