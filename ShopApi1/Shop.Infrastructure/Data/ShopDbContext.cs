
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using Shop.Infrastructure.Identity;
using Shop.Models;


namespace Shop.Data
{
    public class ShopDbContext : IdentityDbContext<AppUser>
    {
        
        public ShopDbContext(DbContextOptions options)

                : base(options)
        { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API
            // Customer
            modelBuilder.Entity<Customer>(
                customer =>
                {

                   
                    customer.HasKey(c => c.Id);
                    customer.Property(c => c.Name)
                        .IsRequired()
                        .HasMaxLength(200);
                    customer.Property(c => c.Email)
                        .HasMaxLength(1000);
                    customer.Property(c => c.PhoneNumber)
                 .HasMaxLength(500);

                    customer.HasMany(c => c.Orders)
                        .WithOne(o => o.customer);
                  
            
                }
                );

            modelBuilder.Entity<Order>(
                Order =>
                {

                    Order.HasKey(o => o.Id);

                    Order.Property(c => c.Product);

                    Order.Property(c => c.TotalSum)
                        .IsRequired();
        
                    Order.HasOne(o => o.customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId);

                }
            );


            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.JwtId).IsUnique();
                entity.Property(e => e.JwtId).IsRequired().HasMaxLength(64);
                entity.Property(e => e.UserId).IsRequired().HasMaxLength(450);
            });





        }

    }




    
}
