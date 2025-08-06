namespace SportGearRental.ViewModels.Review
{
    public class ReviewAdminViewModel
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; } = null!;
        public string SportGearName { get; set; } = null!;
    }
}
