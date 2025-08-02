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
        Task<IEnumerable<RentalViewModel>> GetAllAsync(string userId);
        Task<RentalDetailsViewModel?> GetByIdAsync(Guid id, string userId);
        Task CreateAsync(RentalFormModel model, string userId);
        Task DeleteAsync(Guid id, string userId);
        Task<IEnumerable<SportGearDropdownViewModel>> GetSportGearsForDropdownAsync();
    }
}
