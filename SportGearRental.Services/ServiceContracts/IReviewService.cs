using SportGearRental.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Services.ServiceContracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetReviewsForGearAsync(Guid gearId);
        Task<IEnumerable<ReviewViewModel>> GetAllAsync();
        Task AddReviewAsync(Guid gearId, string userId, ReviewFormModel model);
        Task DeleteAsync(Guid id);
    }
}
