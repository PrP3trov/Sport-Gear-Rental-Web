using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportGearRental.Data;
using SportGearRental.Data.Models;
using SportGearRental.Services;
using SportGearRental.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Test]
        public async Task GetReviewsForGear_ReturnsReviews()
        {
            using var context = CreateContext();
            var service = new ReviewService(context);
            var gearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9");

            var reviews = await service.GetReviewsForGearAsync(gearId);

            Assert.IsTrue(reviews.Any());
        }

        [Test]
        public async Task AddReview_AddsWhenRented()
        {
            using var context = CreateContext();
            var userId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde";
            var category = context.Categories.First();
            var brand = context.Brands.First();
            var condition = context.GearConditions.First();
            var gear = new SportGear
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Desc",
                PricePerDay = 5,
                CategoryId = category.Id,
                BrandId = brand.Id,
                ConditionId = condition.Id,
                OwnerId = userId
            };
            context.SportGears.Add(gear);
            context.Rentals.Add(new Rental
            {
                Id = Guid.NewGuid(),
                SportGearId = gear.Id,
                UserId = userId,
                RentalStartDate = DateTime.UtcNow,
                RentalEndDate = DateTime.UtcNow.AddDays(1),
                TotalPrice = 5
            });
            await context.SaveChangesAsync();

            var service = new ReviewService(context);
            var model = new ReviewFormModel { Content = "Great", Rating = 5 };
            await service.AddReviewAsync(gear.Id, userId, model);

            Assert.IsTrue(context.Reviews.Any(r => r.SportGearId == gear.Id && r.UserId == userId));
        }

        [Test]
        public async Task AddReview_DoesNothingIfNotRented()
        {
            using var context = CreateContext();
            var service = new ReviewService(context);
            var userId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde";
            var gearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9");
            var model = new ReviewFormModel { Content = "Great", Rating = 5 };

            await service.AddReviewAsync(gearId, userId, model);

            Assert.IsFalse(context.Reviews.Any(r => r.SportGearId == gearId && r.UserId == userId));
        }

        [Test]
        public async Task Delete_SetsIsDeleted()
        {
            using var context = CreateContext();
            var review = context.Reviews.First();
            var service = new ReviewService(context);

            await service.DeleteAsync(review.Id);

            Assert.IsTrue(context.Reviews.First(r => r.Id == review.Id).IsDeleted);
        }
    }
}
