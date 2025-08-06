using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportGearRental.Data;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.Services;
using SportGearRental.ViewModels.Review;
using SportGearRental.ViewModels.SportGear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Tests
{
    public class FakeReviewService : IReviewService
    {
        private readonly IEnumerable<ReviewViewModel> _reviews;
        public FakeReviewService(IEnumerable<ReviewViewModel> reviews)
        {
            _reviews = reviews;
        }
        public Task AddReviewAsync(Guid gearId, string userId, ReviewFormModel model) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task<IEnumerable<ReviewViewModel>> GetAllAsync() => Task.FromResult(_reviews);
        public Task<IEnumerable<ReviewViewModel>> GetReviewsForGearAsync(Guid gearId) => Task.FromResult(_reviews);
    }

    [TestFixture]
    public class SportGearServiceTests
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
        public async Task GetAll_ReturnsGears()
        {
            using var context = CreateContext();
            var service = new SportGearService(context, new FakeReviewService(Enumerable.Empty<ReviewViewModel>()));
            var gears = await service.GetAllAsync();
            Assert.IsTrue(gears.Any());
        }

        [Test]
        public async Task GetDetails_IncludesReviews()
        {
            using var context = CreateContext();
            var reviewList = new[] { new ReviewViewModel { Id = Guid.NewGuid(), Content = "Good", Rating = 5, UserName = "User" } };
            var service = new SportGearService(context, new FakeReviewService(reviewList));
            var gearId = context.SportGears.Select(g => g.Id).First();

            var details = await service.GetDetailsByIdAsync(gearId);

            Assert.IsNotNull(details);
            Assert.AreEqual(1, details!.Reviews.Count());
        }

        [Test]
        public async Task GetFormById_ReturnsForOwnerOnly()
        {
            using var context = CreateContext();
            var gear = context.SportGears.First();
            var service = new SportGearService(context, new FakeReviewService(Enumerable.Empty<ReviewViewModel>()));

            var modelOwner = await service.GetFormByIdAsync(gear.Id, gear.OwnerId);
            var modelOther = await service.GetFormByIdAsync(gear.Id, "different-user");

            Assert.IsNotNull(modelOwner);
            Assert.IsNull(modelOther);
        }

        [Test]
        public async Task Create_Edit_Delete_Works()
        {
            using var context = CreateContext();
            var service = new SportGearService(context, new FakeReviewService(Enumerable.Empty<ReviewViewModel>()));
            var category = context.Categories.First();
            var brand = context.Brands.First();
            var condition = context.GearConditions.First();
            var ownerId = "c3d4e5f6-7890-4abc-def1-234567890abc";

            var model = new SportGearFormModel
            {
                Name = "TestGear",
                Description = "Desc",
                PricePerDay = 10,
                ImageUrl = "url",
                CategoryId = category.Id,
                BrandId = brand.Id,
                ConditionId = condition.Id,
                Categories = Enumerable.Empty<CategoryViewModel>(),
                Brands = Enumerable.Empty<BrandViewModel>(),
                Conditions = Enumerable.Empty<GearConditionViewModel>()
            };

            await service.CreateAsync(model, ownerId);
            var gear = context.SportGears.First(g => g.Name == "TestGear");

            model.Name = "Updated";
            await service.EditAsync(gear.Id, model);
            Assert.AreEqual("Updated", context.SportGears.Find(gear.Id)!.Name);

            await service.DeleteAsync(gear.Id);
            Assert.IsTrue(context.SportGears.Find(gear.Id)!.IsDeleted);
        }

        [Test]
        public async Task ExistsAndOwnerChecks()
        {
            using var context = CreateContext();
            var service = new SportGearService(context, new FakeReviewService(Enumerable.Empty<ReviewViewModel>()));
            var gear = context.SportGears.First();

            Assert.IsTrue(await service.ExistsByIdAsync(gear.Id));
            Assert.IsTrue(await service.IsOwnerAsync(gear.Id, gear.OwnerId));
            Assert.IsFalse(await service.IsOwnerAsync(gear.Id, "other"));
        }

        [Test]
        public async Task OptionMethods_ReturnData()
        {
            using var context = CreateContext();
            var service = new SportGearService(context, new FakeReviewService(Enumerable.Empty<ReviewViewModel>()));

            Assert.IsTrue((await service.GetCategoryOptionsAsync()).Any());
            Assert.IsTrue((await service.GetBrandOptionsAsync()).Any());
            Assert.IsTrue((await service.GetConditionOptionsAsync()).Any());
        }

        [Test]
        public async Task GetFiltered_AppliesSearch()
        {
            using var context = CreateContext();
            var service = new SportGearService(context, new FakeReviewService(Enumerable.Empty<ReviewViewModel>()));
            var result = await service.GetFilteredAsync("Yoga", null, null, null, null, null);
            Assert.IsTrue(result.Gears.All(g => g.Name.Contains("Yoga")));
        }
    }
}
