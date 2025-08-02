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

        public async Task<IEnumerable<RentalViewModel>> GetAllAsync()
        {
            return await _context.Rentals
                .Include(r => r.SportGear)
                .Where(r => !r.IsDeleted)
                .Select(r => new RentalViewModel
                {
                    Id = r.Id,
                    SportGearName = r.SportGear.Name,
                    SportGearImageUrl = r.SportGear.ImageUrl,
                    PricePerDay = r.SportGear.PricePerDay,
                    RentalStartDate = r.RentalStartDate,
                    RentalEndDate = r.RentalEndDate
                })
                .ToListAsync();
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
                    StartDate = r.RentalStartDate,
                    EndDate = r.RentalEndDate,
                    Price = r.TotalPrice,
                    UserId = r.UserId
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(RentalFormModel model, string userId)
        {
            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                SportGearId = model.SportGearId,
                RentalStartDate = model.StartDate,
                RentalEndDate = model.EndDate,
                TotalPrice = model.Price,
                UserId = userId
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, RentalFormModel model)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null || rental.IsDeleted) return;

            rental.SportGearId = model.SportGearId;
            rental.RentalStartDate = model.StartDate;
            rental.RentalEndDate = model.EndDate;
            rental.TotalPrice = model.Price;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return;

            rental.IsDeleted = true;
            await _context.SaveChangesAsync();
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
    }
}
