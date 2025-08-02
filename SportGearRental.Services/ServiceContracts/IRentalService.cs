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
        Task<IEnumerable<RentalViewModel>> GetAllAsync();
        Task<RentalDetailsViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(RentalFormModel model, string userId);
        Task UpdateAsync(Guid id, RentalFormModel model);
        Task DeleteAsync(Guid id);

        // Dropdown списък за екипировката
        Task<IEnumerable<SportGearDropdownViewModel>> GetSportGearsForDropdownAsync();
    }
}
