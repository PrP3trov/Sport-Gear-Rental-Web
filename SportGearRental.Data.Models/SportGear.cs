using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.SportGear;
using Microsoft.EntityFrameworkCore;

namespace SportGearRental.Data.Models
{
    public class SportGear
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(GearNameMaxLength)]
        [Comment("The name of the gear")]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        [Comment("The description of the gear")]
        public string Description { get; set; } = null!;

        [Required]
        [Range((double)PricePerDayMin, (double)PricePerDayMax)]
        [Comment("The price per day of the gear")]
        public decimal PricePerDay { get; set; }

        [Comment("The image URL of the gear")]
        public string? ImageUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; } = null!;

        [Required]
        public int ConditionId { get; set; }

        [ForeignKey(nameof(ConditionId))]
        public GearCondition Condition { get; set; } = null!;

        public ICollection<Rental> Rentals { get; set; } = new HashSet<Rental>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
