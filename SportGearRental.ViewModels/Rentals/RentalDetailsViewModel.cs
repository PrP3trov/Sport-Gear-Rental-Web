using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.ViewModels.Rentals
{
    public class RentalDetailsViewModel
    {
        public Guid Id { get; set; }

        public string SportGearName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; } = "Active";

        public string UserId { get; set; } = string.Empty;
    }
}
