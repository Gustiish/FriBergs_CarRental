using FriBergs_CarRental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FriBergs_CarRental.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<CustomerOrder> customerOrders;
        public DbSet<Car> Cars;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CustomerOrder>(entity =>
            {
                entity.HasOne(e => e.ApplicationUser)
                .WithMany(u => u.CustomerOrders)
                .HasForeignKey(e => e.ApplicationUserId)
                .IsRequired();
            });
        }
        public DbSet<FriBergs_CarRental.Models.Car> Car { get; set; } = default!;



    }
}
