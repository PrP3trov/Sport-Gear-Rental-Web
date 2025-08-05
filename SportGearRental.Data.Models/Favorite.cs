using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGearRental.Data.Models
{
    public class Favorite
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public Guid SportGearId { get; set; }

        [ForeignKey(nameof(SportGearId))]
        public virtual SportGear SportGear { get; set; } = null!;

        [Comment("Is the entity deleted (soft delete)?")]
        public bool IsDeleted { get; set; } = false;
    }
}
