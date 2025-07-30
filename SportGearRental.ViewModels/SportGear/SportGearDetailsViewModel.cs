using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.ViewModels.SportGear
{
    public class SportGearDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal PricePerDay { get; set; }
        public string? ImageUrl { get; set; }

        public string Category { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Condition { get; set; } = null!;

        public string OwnerEmail { get; set; } = null!;
    }
}
