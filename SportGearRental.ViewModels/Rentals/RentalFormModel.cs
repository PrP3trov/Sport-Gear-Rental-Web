using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.ViewModels.Rentals
{
    public class RentalFormModel : IValidatableObject
    {
        [Required]
        public Guid SportGearId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public IEnumerable<SportGearDropdownViewModel> SportGears { get; set; }
            = new List<SportGearDropdownViewModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    "Крайната дата не може да бъде преди началната.",
                    new[] { nameof(EndDate) });
            }
        }
    }
}
