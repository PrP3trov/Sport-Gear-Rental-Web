using System;
using System.Collections.Generic;

namespace SportGearRental.ViewModels.SportGear
{
    public class SportGearQueryModel
    {
        public string? SearchTerm { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? ConditionId { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinRating { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public IEnumerable<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();
        public IEnumerable<GearConditionViewModel> Conditions { get; set; } = new List<GearConditionViewModel>();

        public IEnumerable<SportGearListViewModel> Gears { get; set; } = new List<SportGearListViewModel>();
    }
}
