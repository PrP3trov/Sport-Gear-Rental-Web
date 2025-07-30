using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SportGearRental.Common.EntityValidationConstants.SportGear;

namespace SportGearRental.ViewModels.SportGear
{
    public class SportGearFormModel
    {
        [Required]
        [StringLength(GearNameMaxLength, MinimumLength = GearNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range((double)PricePerDayMin, (double)PricePerDayMax)]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Image URL")]
        [Url]
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public Guid BrandId { get; set; }

        [Required]
        [Display(Name = "Condition")]
        public Guid ConditionId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public IEnumerable<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();
        public IEnumerable<GearConditionViewModel> Conditions { get; set; } = new List<GearConditionViewModel>();
    }

    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class BrandViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class GearConditionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
