using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportGearRental.Data.Models;

namespace SportGearRental.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SportGear> SportGears { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<GearCondition> GearConditions { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SportGear>()
                .HasOne(g => g.Category)
                .WithMany(c => c.SportGears)
                .HasForeignKey(g => g.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SportGear>()
                .HasOne(g => g.Brand)
                .WithMany(b => b.SportGears)
                .HasForeignKey(g => g.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SportGear>()
                .HasOne(g => g.Condition)
                .WithMany(c => c.SportGears)
                .HasForeignKey(g => g.ConditionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rental>()
                .Property(r => r.TotalPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
