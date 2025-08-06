using Microsoft.EntityFrameworkCore;
using SportGearRental.Data;
using SportGearRental.Data.Models;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.Rentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Services
{
    public class RentalService : IRentalService
    {
        private readonly ApplicationDbContext _context;

        public RentalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RentalViewModel>> GetAllAsync(string userId)
        {
            return await _context.Rentals
                .Include(r => r.SportGear)
                .Where(r => !r.IsDeleted && r.UserId == userId)
                .Select(r => new RentalViewModel
                {
                    Id = r.Id,
                    SportGearName = r.SportGear.Name,
                    SportGearImageUrl = r.SportGear.ImageUrl ?? string.Empty,
                    PricePerDay = r.SportGear.PricePerDay,
                    RentalStartDate = r.RentalStartDate,
                    RentalEndDate = r.RentalEndDate
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RentalViewModel>> GetAllAsync()
        {
            return await _context.Rentals
                .Include(r => r.SportGear)
                .Where(r => !r.IsDeleted)
                .Select(r => new RentalViewModel
                {
                    Id = r.Id,
                    SportGearName = r.SportGear.Name,
                    SportGearImageUrl = r.SportGear.ImageUrl ?? string.Empty,
                    PricePerDay = r.SportGear.PricePerDay,
                    RentalStartDate = r.RentalStartDate,
                    RentalEndDate = r.RentalEndDate
                })
                .ToListAsync();
        }

        public async Task<RentalDetailsViewModel?> GetByIdAsync(Guid id, string userId)
        {
            return await _context.Rentals
                .Include(r => r.SportGear)
                .Where(r => r.Id == id && !r.IsDeleted && r.UserId == userId)
                .Select(r => new RentalDetailsViewModel
                {
                    Id = r.Id,
                    SportGearName = r.SportGear.Name,
                    SportGearImageUrl = r.SportGear.ImageUrl ?? string.Empty,
                    PricePerDay = r.SportGear.PricePerDay,
                    RentalStartDate = r.RentalStartDate,
                    RentalEndDate = r.RentalEndDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<RentalDetailsViewModel?> GetByIdAsync(Guid id)
        {
            return await _context.Rentals
                .Include(r => r.SportGear)
                .Where(r => r.Id == id && !r.IsDeleted)
                .Select(r => new RentalDetailsViewModel
                {
                    Id = r.Id,
                    SportGearName = r.SportGear.Name,
                    SportGearImageUrl = r.SportGear.ImageUrl ?? string.Empty,
                    PricePerDay = r.SportGear.PricePerDay,
                    RentalStartDate = r.RentalStartDate,
                    RentalEndDate = r.RentalEndDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(RentalFormModel model, string userId)
        {
            var pricePerDay = await _context.SportGears
                .Where(sg => sg.Id == model.SportGearId)
                .Select(sg => sg.PricePerDay)
                .FirstAsync();

            int rentalDays = Math.Max((model.EndDate - model.StartDate).Days, 1);
            decimal totalPrice = pricePerDay * rentalDays;

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                SportGearId = model.SportGearId,
                RentalStartDate = model.StartDate,
                RentalEndDate = model.EndDate,
                TotalPrice = totalPrice,
                UserId = userId,
                IsDeleted = false
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, string userId)
        {
            var rental = await _context.Rentals
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (rental != null)
            {
                rental.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var rental = await _context.Rentals
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rental != null)
            {
                rental.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SportGearDropdownViewModel>> GetSportGearsForDropdownAsync()
        {
            return await _context.SportGears
                .Select(sg => new SportGearDropdownViewModel
                {
                    Id = sg.Id,
                    Name = sg.Name
                })
                .ToListAsync();
        }

        public async Task<bool> HasUserRentedGearAsync(Guid gearId, string userId)
        {
            return await _context.Rentals
                .AnyAsync(r => !r.IsDeleted && r.SportGearId == gearId && r.UserId == userId);
        }
    }
}
