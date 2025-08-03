using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.Review;

namespace SportGearRental.ViewModels.Review
{
    public class ReviewFormModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;

        [Required]
        [Range(RatingMin, RatingMax)]
        public int Rating { get; set; }
    }
}
