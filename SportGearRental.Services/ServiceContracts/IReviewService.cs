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
        Task AddReviewAsync(Guid gearId, string userId, ReviewFormModel model);
        Task<IEnumerable<ReviewAdminViewModel>> GetAllAsync();
        Task<ReviewFormModel?> GetByIdAsync(Guid id);
        Task EditAsync(Guid id, ReviewFormModel model);
        Task DeleteAsync(Guid id);
    }
}
