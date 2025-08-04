using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.ViewModels.Rentals
{
    public class RentalViewModel
    {
        public Guid Id { get; set; }
        public string SportGearName { get; set; } = string.Empty;
        public string SportGearImageUrl { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }

        public string UserName { get; set; } = string.Empty;
        public int DaysRemaining => Math.Max((RentalEndDate - DateTime.Now).Days, 0);
    }
}
