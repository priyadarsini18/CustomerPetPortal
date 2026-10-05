using CustomerPetPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerPetPortal.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<CustomerPolicy> CustomerPolicies { get; set; }

        public DbSet<CustomerClaim> CustomerClaims { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primary Keys
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Address>()
                .HasKey(a => a.AddressId);

            modelBuilder.Entity<Pet>()
                .HasKey(p => p.PetId);

            modelBuilder.Entity<CustomerPolicy>()
                .HasKey(p => p.PolicyId);

            modelBuilder.Entity<CustomerClaim>()
                .HasKey(c => c.ClaimId);

            // Customer -> Pets
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Pets)
                .HasForeignKey(p => p.CustomerId);

            // Customer -> Address
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CustomerId);
        }
    }
}