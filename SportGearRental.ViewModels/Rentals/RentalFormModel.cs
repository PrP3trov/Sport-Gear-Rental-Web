using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.ViewModels.Rentals
{
    public class RentalFormModel
    {
        [Required]
        [Display(Name = "Sport Gear")]
        public Guid SportGearId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, 9999.99)]
        public decimal Price { get; set; }

        // Dropdown за избор на екипировка
        public IEnumerable<SportGearDropdownViewModel> SportGears { get; set; }
            = new List<SportGearDropdownViewModel>();
    }
}
