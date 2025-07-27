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
        public int Id { get; set; }

        [MaxLength(ContentMaxLength)]
        public string? Content { get; set; }

        [Range(RatingMin, RatingMax)]
        public int Rating { get; set; }

        public string? UserId { get; set; }

        [ForeignKey(nameof(SportGear))]
        public int SportGearId { get; set; }
        public SportGear SportGear { get; set; } = null!;
    }
}
