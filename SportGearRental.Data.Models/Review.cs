using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.Review;


namespace SportGearRental.Data.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(ContentMaxLength)]
        public string? Content { get; set; }

        [Required]
        [Range(RatingMin, RatingMax)]
        public int Rating { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        [Required]
        public Guid SportGearId { get; set; }

        [ForeignKey(nameof(SportGearId))]
        public SportGear SportGear { get; set; } = null!;
    }
}
