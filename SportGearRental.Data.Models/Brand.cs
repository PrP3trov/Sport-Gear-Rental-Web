using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.Brand;

namespace SportGearRental.Data.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(BrandNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<SportGear> SportGears { get; set; } = new HashSet<SportGear>();
    }
}
