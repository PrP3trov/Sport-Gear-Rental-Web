using Microsoft.EntityFrameworkCore;
using SportGearRental.Data;
using SportGearRental.Data.Models;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.SportGear;

namespace SportGearRental.Services
{
    public class SportGearService : ISportGearService
    {
        private readonly ApplicationDbContext _context;
        private readonly IReviewService _reviewService;

        public SportGearService(ApplicationDbContext context, IReviewService reviewService)
        {
            _context = context;
            _reviewService = reviewService;
        }

        public async Task<IEnumerable<SportGearListViewModel>> GetAllAsync()
        {
            return await _context.SportGears
                .Where(g => !g.IsDeleted)
                .Select(g => new SportGearListViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageUrl = g.ImageUrl ?? "",
                    PricePerDay = g.PricePerDay,
                    OwnerId = g.OwnerId 
                })
                .ToListAsync();
        }

        public async Task<SportGearDetailsViewModel?> GetDetailsByIdAsync(Guid id)
        {
            var model =  await _context.SportGears
                .Where(g => g.Id == id && !g.IsDeleted)
                .Select(g => new SportGearDetailsViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    PricePerDay = g.PricePerDay,
                    ImageUrl = g.ImageUrl,
                    Category = g.Category.Name,
                    Brand = g.Brand.Name,
                    Condition = g.Condition.Name,
                    OwnerEmail = g.Owner.Email,
                    OwnerId = g.OwnerId 
                })
                .FirstOrDefaultAsync();

            if (model != null)
            {
                model.Reviews = await _reviewService.GetReviewsForGearAsync(id);
            }

            return model;
        }

        public async Task<SportGearFormModel?> GetFormByIdAsync(Guid id, string? userId)
        {
            var query = _context.SportGears
                            .Where(g => g.Id == id && !g.IsDeleted);

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(g => g.OwnerId == userId);
            }

            var gear = await query.FirstOrDefaultAsync();

            if (gear == null) return null;

            return new SportGearFormModel
            {
                Name = gear.Name,
                Description = gear.Description,
                PricePerDay = gear.PricePerDay,
                ImageUrl = gear.ImageUrl,
                CategoryId = gear.CategoryId,
                BrandId = gear.BrandId,
                ConditionId = gear.ConditionId,
                Categories = await GetCategoryOptionsAsync(),
                Brands = await GetBrandOptionsAsync(),
                Conditions = await GetConditionOptionsAsync()
            };
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _context.SportGears.AnyAsync(g => g.Id == id && !g.IsDeleted);
        }

        public async Task<bool> IsOwnerAsync(Guid id, string userId)
        {
            return await _context.SportGears.AnyAsync(g => g.Id == id && g.OwnerId == userId && !g.IsDeleted);
        }

        public async Task CreateAsync(SportGearFormModel model, string ownerId)
        {
            var gear = new SportGear
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                PricePerDay = model.PricePerDay,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                ConditionId = model.ConditionId,
                OwnerId = ownerId
            };

            await _context.SportGears.AddAsync(gear);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Guid id, SportGearFormModel model)
        {
            var gear = await _context.SportGears.FindAsync(id);
            if (gear == null || gear.IsDeleted) return;

            gear.Name = model.Name;
            gear.Description = model.Description;
            gear.PricePerDay = model.PricePerDay;
            gear.ImageUrl = model.ImageUrl;
            gear.CategoryId = model.CategoryId;
            gear.BrandId = model.BrandId;
            gear.ConditionId = model.ConditionId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var gear = await _context.SportGears.FindAsync(id);
            if (gear == null) return;

            gear.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoryOptionsAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BrandViewModel>> GetBrandOptionsAsync()
        {
            return await _context.Brands
                .Select(b => new BrandViewModel
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GearConditionViewModel>> GetConditionOptionsAsync()
        {
            return await _context.GearConditions
                .Select(c => new GearConditionViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<SportGearQueryModel> GetFilteredAsync(
            string? searchTerm,
            Guid? categoryId,
            Guid? brandId,
            Guid? conditionId,
            decimal? maxPrice,
            int? minRating)
        {
            var query = _context.SportGears
                .Include(g => g.Category)
                .Include(g => g.Brand)
                .Include(g => g.Condition)
                .Include(g => g.Reviews)
                .Where(g => !g.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string lowerTerm = searchTerm.ToLower();
                query = query.Where(g =>
                    g.Name.ToLower().Contains(lowerTerm) ||
                    g.Description.ToLower().Contains(lowerTerm));
            }

            if (categoryId.HasValue && categoryId != Guid.Empty)
                query = query.Where(g => g.CategoryId == categoryId);

            if (brandId.HasValue && brandId != Guid.Empty)
                query = query.Where(g => g.BrandId == brandId);

            if (conditionId.HasValue && conditionId != Guid.Empty)
                query = query.Where(g => g.ConditionId == conditionId);

            if (maxPrice.HasValue)
                query = query.Where(g => g.PricePerDay <= maxPrice.Value);

            if (minRating.HasValue && minRating > 0)
                query = query.Where(g => g.Reviews.Any() &&
                                         g.Reviews.Average(r => r.Rating) >= minRating);

            var gears = await query
                .Select(g => new SportGearListViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageUrl = g.ImageUrl ?? "",
                    PricePerDay = g.PricePerDay,
                    OwnerId = g.OwnerId
                })
                .ToListAsync();

            return new SportGearQueryModel
            {
                SearchTerm = searchTerm,
                CategoryId = categoryId,
                BrandId = brandId,
                ConditionId = conditionId,
                MaxPrice = maxPrice,
                MinRating = minRating,
                Categories = await _context.Categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }).ToListAsync(),
                Brands = await _context.Brands.Select(b => new BrandViewModel { Id = b.Id, Name = b.Name }).ToListAsync(),
                Conditions = await _context.GearConditions.Select(gc => new GearConditionViewModel { Id = gc.Id, Name = gc.Name }).ToListAsync(),
                Gears = gears
            };
        }
    }
}
