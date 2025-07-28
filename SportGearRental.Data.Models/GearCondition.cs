using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.GearCondition;


namespace SportGearRental.Data.Models
{
    public class GearCondition
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(GearConditionNameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(GearDescriptionMaxLength)]
        public string? Description { get; set; }

        public virtual ICollection<SportGear> SportGears { get; set; } = new HashSet<SportGear>();
    }
}
