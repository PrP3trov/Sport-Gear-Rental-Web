using Microsoft.EntityFrameworkCore;
using SportGearRental.Data;
using SportGearRental.Data.Models;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviewsForGearAsync(Guid gearId)
        {
            return await _context.Reviews
                .Where(r => r.SportGearId == gearId)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    Rating = r.Rating,
                    UserName = r.User.UserName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewAdminViewModel>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.SportGear)
                .Include(r => r.User)
                .Select(r => new ReviewAdminViewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    Rating = r.Rating,
                    UserName = r.User.UserName,
                    SportGearName = r.SportGear.Name
                })
                .ToListAsync();
        }

        public async Task<ReviewFormModel?> GetByIdAsync(Guid id)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return null;

            return new ReviewFormModel
            {
                Content = review.Content,
                Rating = review.Rating
            };
        }

        public async Task EditAsync(Guid id, ReviewFormModel model)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return;

            review.Content = model.Content;
            review.Rating = model.Rating;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return;

            review.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task AddReviewAsync(Guid gearId, string userId, ReviewFormModel model)
        {
            bool hasRented = await _context.Rentals
                .AnyAsync(r => r.SportGearId == gearId && r.UserId == userId);

            if (!hasRented)
            {
                return;
            }

            bool alreadyReviewed = await _context.Reviews
                .AnyAsync(r => r.SportGearId == gearId && r.UserId == userId);

            if (alreadyReviewed)
            {
                return;
            }

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                Rating = model.Rating,
                SportGearId = gearId,
                UserId = userId
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
