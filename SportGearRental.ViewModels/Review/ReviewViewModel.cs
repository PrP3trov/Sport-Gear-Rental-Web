using System;

namespace SportGearRental.ViewModels.Review
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }

        public string? Content { get; set; }

        public int Rating { get; set; }

        public string UserName { get; set; } = null!;
    }
}