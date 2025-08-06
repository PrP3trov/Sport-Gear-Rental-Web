using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using SportGearRental.Data;
using SportGearRental.Data.Models;
using SportGearRental.Services;
using SportGearRental.ViewModels.Rentals;

namespace SportGearRental.Tests
{
    [TestFixture]
    public class FavoriteServiceTests
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
        public async Task AddFavorite_AddsRecord()
        {
            using var context = CreateContext();
            var service = new FavoriteService(context);
            var userId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde";
            var gearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9");

            await service.AddFavoriteAsync(userId, gearId);

            Assert.AreEqual(1, context.Favorites.Count());
        }

        [Test]
        public async Task AddFavorite_DoesNotDuplicate()
        {
            using var context = CreateContext();
            var service = new FavoriteService(context);
            var userId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde";
            var gearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9");

            await service.AddFavoriteAsync(userId, gearId);
            await service.AddFavoriteAsync(userId, gearId);

            Assert.AreEqual(1, context.Favorites.Count());
        }

        [Test]
        public async Task RemoveFavorite_MarksAsDeleted()
        {
            using var context = CreateContext();
            var service = new FavoriteService(context);
            var userId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde";
            var gearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9");

            await service.AddFavoriteAsync(userId, gearId);
            await service.RemoveFavoriteAsync(userId, gearId);

            var favorite = context.Favorites.First();
            Assert.IsTrue(favorite.IsDeleted);
        }

        [Test]
        public async Task GetFavorites_ReturnsAddedFavorite()
        {
            using var context = CreateContext();
            var service = new FavoriteService(context);
            var userId = "d4e5f6a7-8901-4bcd-efa2-34567890bcde";
            var gearId = Guid.Parse("8f14e45f-ceea-4bfc-9274-b7987d4a59d9");

            await service.AddFavoriteAsync(userId, gearId);
            var favorites = await service.GetFavoritesAsync(userId);

            Assert.AreEqual(1, favorites.Count());
            Assert.AreEqual(gearId, favorites.First().Id);
        }
    }
}