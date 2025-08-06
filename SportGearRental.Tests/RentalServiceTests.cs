using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SportGearRental.Data;
using SportGearRental.Services;
using SportGearRental.ViewModels.Rentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Tests
{
    [TestFixture]
    public class RentalServiceTests
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
        public async Task Create_AddsRentalWithPrice()
        {
            using var context = CreateContext();
            var service = new RentalService(context);
            var userId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde";
            var gearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9");
            var start = new DateTime(2025, 1, 1);
            var end = start.AddDays(2);
            var model = new RentalFormModel { SportGearId = gearId, StartDate = start, EndDate = end };

            await service.CreateAsync(model, userId);

            var rental = context.Rentals.First(r => r.SportGearId == gearId && r.UserId == userId && r.RentalStartDate == start);
            Assert.AreEqual((end - start).Days * context.SportGears.Find(gearId)!.PricePerDay, rental.TotalPrice);
        }

        [Test]
        public async Task GetAll_FiltersByUser()
        {
            using var context = CreateContext();
            var service = new RentalService(context);
            var all = await service.GetAllAsync();
            Assert.AreEqual(3, all.Count());

            var userId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde";
            var userRentals = await service.GetAllAsync(userId);
            Assert.IsTrue(userRentals.All(r => r.UserName != null));
            Assert.AreEqual(2, userRentals.Count());
        }

        [Test]
        public async Task Edit_UpdatesRental()
        {
            using var context = CreateContext();
            var service = new RentalService(context);
            var rental = context.Rentals.First();
            var newGear = context.SportGears.First(g => g.Id != rental.SportGearId);
            var model = new RentalFormModel { SportGearId = newGear.Id, StartDate = rental.RentalStartDate.AddDays(1), EndDate = rental.RentalEndDate.AddDays(1) };

            await service.EditAsync(rental.Id, model);

            var updated = context.Rentals.Find(rental.Id)!;
            Assert.AreEqual(newGear.Id, updated.SportGearId);
            Assert.AreEqual(model.EndDate, updated.RentalEndDate);
        }

        [Test]
        public async Task Delete_MarksIsDeleted()
        {
            using var context = CreateContext();
            var service = new RentalService(context);
            var rental = context.Rentals.First();

            await service.DeleteAsync(rental.Id);

            Assert.IsTrue(context.Rentals.Find(rental.Id)!.IsDeleted);
        }

        [Test]
        public async Task HasUserRentedGear_ReturnsTrueWhenExists()
        {
            using var context = CreateContext();
            var service = new RentalService(context);
            var rental = context.Rentals.First();

            bool result = await service.HasUserRentedGearAsync(rental.SportGearId, rental.UserId);
            Assert.IsTrue(result);
        }
    }
}
