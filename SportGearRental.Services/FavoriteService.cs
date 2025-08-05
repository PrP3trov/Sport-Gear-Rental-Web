using SportGearRental.Data.Models;
using SportGearRental.Data;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.SportGear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportGearRental.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddFavoriteAsync(string userId, Guid gearId)
        {
            bool exists = await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.SportGearId == gearId);
            if (!exists)
            {
                var favorite = new Favorite
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    SportGearId = gearId
                };
                _context.Favorites.Add(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFavoriteAsync(string userId, Guid gearId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.SportGearId == gearId);
            if (favorite != null)
            {
                favorite.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SportGearListViewModel>> GetFavoritesAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => new SportGearListViewModel
                {
                    Id = f.SportGear.Id,
                    Name = f.SportGear.Name,
                    ImageUrl = f.SportGear.ImageUrl ?? string.Empty,
                    PricePerDay = f.SportGear.PricePerDay,
                    OwnerId = f.SportGear.OwnerId
                })
                .ToListAsync();
        }
    }
}
