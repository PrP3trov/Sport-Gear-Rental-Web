using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.Rental;
using Microsoft.EntityFrameworkCore;


namespace SportGearRental.Data.Models
{
    public class Rental
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } =  null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public Guid SportGearId { get; set; }

        [ForeignKey(nameof(SportGearId))]
        public virtual SportGear SportGear { get; set; } = null!;

        [Required]
        public DateTime RentalStartDate { get; set; }

        [Required]
        public DateTime RentalEndDate { get; set; }

        [Comment("Is the entity deleted (soft delete)?")]
        public bool IsDeleted { get; set; } = false;

        [Required]
        [Range((double)TotalPriceMin, (double)TotalPriceMax)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
