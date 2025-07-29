using Microsoft.AspNetCore.Identity;
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

        public virtual DbSet<SportGear> SportGears { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<GearCondition> GearConditions { get; set; } = null!;
        public virtual DbSet<Rental> Rentals { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

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

                        builder.Entity<SportGear>().HasQueryFilter(g => !g.IsDeleted);
            builder.Entity<Rental>().HasQueryFilter(r => !r.IsDeleted);
            builder.Entity<Review>().HasQueryFilter(r => !r.IsDeleted);

            // Роли
            var adminRoleId = "a1f2e3d4-c5b6-47f8-9876-123456789abc";
            var userRoleId = "b2f3e4d5-a6b7-48c9-8765-abcdef123456";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = "c3d4e5f6-7890-4abc-def1-234567890abc",
                UserName = "admin@gear.bg",
                NormalizedUserName = "ADMIN@GEAR.BG",
                Email = "admin@gear.bg",
                NormalizedEmail = "ADMIN@GEAR.BG",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!");

            var normalUser = new ApplicationUser
            {
                Id = "d4e5f6a7-8901-4bcd-efa2-34567890bcde",
                UserName = "user@gear.bg",
                NormalizedUserName = "USER@GEAR.BG",
                Email = "user@gear.bg",
                NormalizedEmail = "USER@GEAR.BG",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            normalUser.PasswordHash = hasher.HashPassword(normalUser, "User123!");

            builder.Entity<ApplicationUser>().HasData(adminUser, normalUser);

            // Свързване потребители с роли
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUser.Id
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = normalUser.Id
                }
            );


            // Seed Categories - 5
            builder.Entity<Category>().HasData(
                new Category { Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"), Name = "Обувки" },
                new Category { Id = Guid.Parse("9c858901-8a57-4791-81fe-4c455b099bc9"), Name = "Дрехи" },
                new Category { Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), Name = "Аксесоари" },
                new Category { Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Name = "Фитнес оборудване" },
                new Category { Id = Guid.Parse("6fa459ea-ee8a-3ca4-894e-db77e160355e"), Name = "Велосипеди" }
            );

            // Seed Brands - 5
            builder.Entity<Brand>().HasData(
                new Brand { Id = Guid.Parse("c9bf9e57-1685-4c89-bafb-ff5af830be8a"), Name = "Найк" },
                new Brand { Id = Guid.Parse("e358efa4-1e22-4ac1-8f98-cd78e9a6ccf3"), Name = "Адидас" },
                new Brand { Id = Guid.Parse("3d6f0a88-9d64-4a38-9f2c-52deff0a92d2"), Name = "Пума" },
                new Brand { Id = Guid.Parse("1c2d0d89-62e6-4e3b-8fcd-125c5bb8f2a1"), Name = "Колумбия" },
                new Brand { Id = Guid.Parse("0d9a5b76-48b1-4eea-8c1d-cdff4a56b57a"), Name = "Саломон" }
            );

            // Seed GearConditions - 5
            builder.Entity<GearCondition>().HasData(
                new GearCondition { Id = Guid.Parse("f1a52b5b-4c3d-44d0-80f8-5ad836eeb09c"), Name = "Ново", Description = "Изцяло ново, неизползвано" },
                new GearCondition { Id = Guid.Parse("862f2c20-cb09-4e6a-b4d2-92d0d4e3d5f6"), Name = "Като ново", Description = "Много леко използвано" },
                new GearCondition { Id = Guid.Parse("73c2f799-3e94-47bc-8c29-1d157f243bbc"), Name = "Добро", Description = "Използвано, но в добро състояние" },
                new GearCondition { Id = Guid.Parse("22a963d9-2a3b-4baf-943d-9e6a51b8db78"), Name = "Средно", Description = "С видими следи от употреба" },
                new GearCondition { Id = Guid.Parse("5f0e3e1f-4a76-4c4e-9d34-bdeff33f76a9"), Name = "Лошо", Description = "Със сериозни дефекти" }
            );

            // Seed 10 SportGears 
            builder.Entity<SportGear>().HasData(
                new SportGear
                {
                    Id = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9"),
                    Name = "Nike Air Zoom Pegasus",
                    Description = "Леки маратонки за бягане с въздушна възглавница.",
                    PricePerDay = 12.50m,
                    ImageUrl = "https://example.com/images/nike-pegasus.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    BrandId = Guid.Parse("c9bf9e57-1685-4c89-bafb-ff5af830be8a"),
                    ConditionId = Guid.Parse("f1a52b5b-4c3d-44d0-80f8-5ad836eeb09c")
                },
                new SportGear
                {
                    Id = Guid.Parse("acbd18db-4cc2-43e2-a05d-dcbbd298db96"),
                    Name = "Adidas Terrex Jacket",
                    Description = "Водоустойчиво яке за планина.",
                    PricePerDay = 15.00m,
                    ImageUrl = "https://example.com/images/adidas-terrex.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("9c858901-8a57-4791-81fe-4c455b099bc9"),
                    BrandId = Guid.Parse("e358efa4-1e22-4ac1-8f98-cd78e9a6ccf3"),
                    ConditionId = Guid.Parse("862f2c20-cb09-4e6a-b4d2-92d0d4e3d5f6")
                },
                new SportGear
                {
                    Id = Guid.Parse("37b51d19-59a7-4ed4-8996-0b1d0c428a92"),
                    Name = "Puma Fitness Gloves",
                    Description = "Удобни ръкавици за фитнес и тежести.",
                    PricePerDay = 5.00m,
                    ImageUrl = "https://example.com/images/puma-gloves.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    BrandId = Guid.Parse("3d6f0a88-9d64-4a38-9f2c-52deff0a92d2"),
                    ConditionId = Guid.Parse("73c2f799-3e94-47bc-8c29-1d157f243bbc")
                },
                new SportGear
                {
                    Id = Guid.Parse("73feffa4-7f1b-4e14-90c6-b42b041bf63f"),
                    Name = "Columbia Winter Gloves",
                    Description = "Топли ръкавици за зимни спортове.",
                    PricePerDay = 7.50m,
                    ImageUrl = "https://example.com/images/columbia-gloves.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    BrandId = Guid.Parse("1c2d0d89-62e6-4e3b-8fcd-125c5bb8f2a1"),
                    ConditionId = Guid.Parse("73c2f799-3e94-47bc-8c29-1d157f243bbc")
                },
                new SportGear
                {
                    Id = Guid.Parse("1dcca233-c2a1-4f1e-9d9f-c2147b0ccf8a"),
                    Name = "Salomon Ski Goggles",
                    Description = "Очила за ски и сноуборд с високо качество.",
                    PricePerDay = 10.00m,
                    ImageUrl = "https://example.com/images/salomon-goggles.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    BrandId = Guid.Parse("0d9a5b76-48b1-4eea-8c1d-cdff4a56b57a"),
                    ConditionId = Guid.Parse("f1a52b5b-4c3d-44d0-80f8-5ad836eeb09c")
                },
                new SportGear
                {
                    Id = Guid.Parse("b6d81b36-1b9e-4f1d-9e0f-5b3a9d3b21d6"),
                    Name = "Nike Running Shorts",
                    Description = "Дишащи шорти за бягане и спорт.",
                    PricePerDay = 6.00m,
                    ImageUrl = "https://example.com/images/nike-shorts.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("9c858901-8a57-4791-81fe-4c455b099bc9"),
                    BrandId = Guid.Parse("c9bf9e57-1685-4c89-bafb-ff5af830be8a"),
                    ConditionId = Guid.Parse("862f2c20-cb09-4e6a-b4d2-92d0d4e3d5f6")
                },
                new SportGear
                {
                    Id = Guid.Parse("a87ff679-a2f3-4f54-8e8f-0fa6d8b7cd55"),
                    Name = "Adidas Fitness Mat",
                    Description = "Удобен постел за фитнес и йога.",
                    PricePerDay = 8.00m,
                    ImageUrl = "https://example.com/images/adidas-mat.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    BrandId = Guid.Parse("e358efa4-1e22-4ac1-8f98-cd78e9a6ccf3"),
                    ConditionId = Guid.Parse("f1a52b5b-4c3d-44d0-80f8-5ad836eeb09c")
                },
                new SportGear
                {
                    Id = Guid.Parse("e4da3b7f-bbce-4a1b-b0f4-35e1d9f2b35a"),
                    Name = "Mountain Bike X200",
                    Description = "Велосипед с 21 скорости и амортисьори.",
                    PricePerDay = 25.00m,
                    ImageUrl = "https://example.com/images/bike-x200.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("6fa459ea-ee8a-3ca4-894e-db77e160355e"),
                    BrandId = Guid.Parse("1c2d0d89-62e6-4e3b-8fcd-125c5bb8f2a1"),
                    ConditionId = Guid.Parse("73c2f799-3e94-47bc-8c29-1d157f243bbc")
                },
                new SportGear
                {
                    Id = Guid.Parse("1679091c-5a88-4e3e-96a4-7f3b3e7d9d3f"),
                    Name = "Yoga Mat Deluxe",
                    Description = "Дебела постелка за йога с антислип покритие.",
                    PricePerDay = 9.00m,
                    ImageUrl = "https://example.com/images/yoga-mat-deluxe.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    BrandId = Guid.Parse("0d9a5b76-48b1-4eea-8c1d-cdff4a56b57a"),
                    ConditionId = Guid.Parse("862f2c20-cb09-4e6a-b4d2-92d0d4e3d5f6")
                },
                new SportGear
                {
                    Id = Guid.Parse("8e296a06-2b87-4f7d-bb57-1a7b1c5ca6e9"),
                    Name = "Trail Running Shoes",
                    Description = "Обувки за бягане в планината, устойчиви на кал и вода.",
                    PricePerDay = 13.50m,
                    ImageUrl = "https://example.com/images/trail-shoes.jpg",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    BrandId = Guid.Parse("3d6f0a88-9d64-4a38-9f2c-52deff0a92d2"),
                    ConditionId = Guid.Parse("73c2f799-3e94-47bc-8c29-1d157f243bbc")
                }
            );

            // Reviews
            builder.Entity<Review>().HasData(
                // 3 ревюта на user
                new Review
                {
                    Id = Guid.Parse("f2e3a1b4-7f9c-4d26-bf6e-0a4eae9d0bfb"),
                    Content = "Great equipment, very durable!",
                    Rating = 5,
                    UserId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde", // нормален потребител
                    SportGearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9"), // Nike Air Zoom Pegasus
                    IsDeleted = false
                },
                new Review
                {
                    Id = Guid.Parse("6d7f2c91-ea12-4c88-9b3a-8f7d5a0e4b9d"),
                    Content = "Good value for money.",
                    Rating = 4,
                    UserId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde",
                    SportGearId = Guid.Parse("acbd18db-4cc2-43e2-a05d-dcbbd298db96"), // Adidas Terrex Jacket
                    IsDeleted = false
                },
                new Review
                {
                    Id = Guid.Parse("c9f1d8b2-3a47-4d89-8e7c-12a9d1e8f3b2"),
                    Content = "Works perfectly for my needs.",
                    Rating = 5,
                    UserId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde",
                    SportGearId = Guid.Parse("37b51d19-59a7-4ed4-8996-0b1d0c428a92"), // Puma Fitness Gloves
                    IsDeleted = false
                },

                // 3 ревюта на admin
                new Review
                {
                    Id = Guid.Parse("d4b6c1a3-7f56-4a2e-9bc7-efa7a1234567"),
                    Content = "Highly recommend this gear!",
                    Rating = 5,
                    UserId = "c3d4e5f6-7890-4abc-def1-234567890abc", // admin user
                    SportGearId = Guid.Parse("b6d81b36-1b9e-4f1d-9e0f-5b3a9d3b21d6"), // Nike Running Shorts
                    IsDeleted = false
                },
                new Review
                {
                    Id = Guid.Parse("e3f4d2b1-1234-4c5d-a789-6b7c8d9e0f1a"),
                    Content = "Good quality and fast delivery.",
                    Rating = 4,
                    UserId = "c3d4e5f6-7890-4abc-def1-234567890abc",
                    SportGearId = Guid.Parse("a87ff679-a2f3-4f54-8e8f-0fa6d8b7cd55"), // Adidas Fitness Mat
                    IsDeleted = false
                },
                new Review
                {
                    Id = Guid.Parse("f7e8d9c0-4567-4b8d-b12f-34a5b6c7d8e9"),
                    Content = "Perfect for winter sports.",
                    Rating = 5,
                    UserId = "c3d4e5f6-7890-4abc-def1-234567890abc",
                    SportGearId = Guid.Parse("73feffa4-7f1b-4e14-90c6-b42b041bf63f"), // Columbia Winter Gloves
                    IsDeleted = false
                }
            );

            builder.Entity<Rental>().HasData(
                new Rental
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                    UserId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde", // примерен потребител
                    SportGearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9"), // примерен спортен артикул
                    RentalStartDate = new DateTime(2025, 7, 20),
                    RentalEndDate = new DateTime(2025, 7, 25),
                    IsDeleted = false,
                    TotalPrice = 50.00m
                },
                new Rental
                {
                    Id = Guid.Parse("b2c3d4e5-f6a7-48b9-9c0d-1e2f3a4b5c6d"),
                    UserId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde",
                    SportGearId = Guid.Parse("acbd18db-4cc2-43e2-a05d-dcbbd298db96"),
                    RentalStartDate = new DateTime(2025, 7, 27),
                    RentalEndDate = new DateTime(2025, 8, 1),
                    IsDeleted = false,
                    TotalPrice = 75.50m
                },
                new Rental
                {
                    Id = Guid.Parse("c3d4e5f6-a7b8-49c0-ad1e-2f3a4b5c6d7e"),
                    UserId = "c3d4e5f6-7890-4abc-def1-234567890abc",
                    SportGearId = Guid.Parse("b6d81b36-1b9e-4f1d-9e0f-5b3a9d3b21d6"),
                    RentalStartDate = new DateTime(2025, 7, 15),
                    RentalEndDate = new DateTime(2025, 7, 18),
                    IsDeleted = false,
                    TotalPrice = 30.00m
                }
            );
        }
    }
}
