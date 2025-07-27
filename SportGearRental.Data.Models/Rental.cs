using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.Rental;


namespace SportGearRental.Data.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public int SportGearId { get; set; }

        [ForeignKey(nameof(SportGearId))]
        public SportGear SportGear { get; set; } = null!;

        [Required]
        public DateTime RentalStartDate { get; set; }

        [Required]
        public DateTime RentalEndDate { get; set; }

        [Required]
        [Range((double)TotalPriceMin, (double)TotalPriceMax)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
