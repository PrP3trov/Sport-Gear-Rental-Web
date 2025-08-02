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

        public int DaysRemaining
        {
            get
            {
                var remaining = (RentalEndDate - DateTime.Now).Days;
                return remaining < 0 ? 0 : remaining;
            }
        }
    }
}
