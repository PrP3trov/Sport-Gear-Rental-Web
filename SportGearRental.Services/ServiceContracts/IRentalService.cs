using SportGearRental.ViewModels.Rentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Services.ServiceContracts
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalViewModel>> GetAllAsync(string? userId = null);
        Task<RentalDetailsViewModel?> GetByIdAsync(Guid id, string? userId = null);
        Task CreateAsync(RentalFormModel model, string userId);
        Task<RentalFormModel?> GetFormByIdAsync(Guid id, string? userId = null);
        Task EditAsync(Guid id, RentalFormModel model, string? userId = null);
        Task DeleteAsync(Guid id, string? userId = null);
        Task<IEnumerable<SportGearDropdownViewModel>> GetSportGearsForDropdownAsync();
        Task<bool> HasUserRentedGearAsync(Guid gearId, string userId);

    }
}
