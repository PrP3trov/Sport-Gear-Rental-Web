using SportGearRental.ViewModels.SportGear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Services.ServiceContracts
{
    public interface IFavoriteService
    {
        Task AddFavoriteAsync(string userId, Guid gearId);
        Task RemoveFavoriteAsync(string userId, Guid gearId);
        Task<IEnumerable<SportGearListViewModel>> GetFavoritesAsync(string userId);
    }
}
