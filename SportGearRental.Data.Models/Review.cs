using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.Review;
using Microsoft.EntityFrameworkCore;


namespace SportGearRental.Data.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(ContentMaxLength)]
        public string? Content { get; set; }

        [Comment("Is the entity deleted (soft delete)?")]
        public bool IsDeleted { get; set; } = false;

        [Required]
        [Range(RatingMin, RatingMax)]
        public int Rating { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public Guid SportGearId { get; set; }

        [ForeignKey(nameof(SportGearId))]
        public virtual SportGear SportGear { get; set; } = null!;
    }
}
