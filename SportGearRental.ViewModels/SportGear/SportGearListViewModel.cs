using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.ViewModels.SportGear
{
    public class SportGearListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal PricePerDay { get; set; }

        public string OwnerId { get; set; } = null!;
    }
}
