using SportGearRental.Data.Models;
using SportGearRental.ViewModels.SportGear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Services.ServiceContracts
{
    public interface ISportGearService
    {
        Task<IEnumerable<SportGearListViewModel>> GetAllAsync();
        Task<SportGearDetailsViewModel?> GetDetailsByIdAsync(Guid id);
        Task<SportGearFormModel?> GetFormByIdAsync(Guid id, string userId);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<bool> IsOwnerAsync(Guid id, string userId);

        Task CreateAsync(SportGearFormModel model, string ownerId);
        Task EditAsync(Guid id, SportGearFormModel model);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<CategoryViewModel>> GetCategoryOptionsAsync();
        Task<IEnumerable<BrandViewModel>> GetBrandOptionsAsync();
        Task<IEnumerable<GearConditionViewModel>> GetConditionOptionsAsync();

    }
}
