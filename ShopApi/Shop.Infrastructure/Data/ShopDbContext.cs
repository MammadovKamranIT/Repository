
using Shop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Shop.Data
{
    public class ShopDbContext : IdentityDbContext
    {
        
        public ShopDbContext(DbContextOptions options)

                : base(options)
        { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();

 




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
                        .WithOne(o => o.Customer)
                        .HasForeignKey(o => o.CustomerId);
            
                }
                );

            modelBuilder.Entity<Order>(
                Order =>
                {

                    Order.HasKey(o => o.Id);

                    Order.Property(c => c.CustomerId)
                        .IsRequired();
                    Order.Property(c => c.Product);

                    Order.Property(c => c.TotalSum)
                        .IsRequired();
                    Order.Property(c => c.Customer)
                        .IsRequired();

                }
            );





        }

    }




    
}
