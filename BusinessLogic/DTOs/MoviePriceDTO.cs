namespace BusinessLogic.DTOs
{
    public class MoviePriceDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }

        // data to map:
        public string? MovieName { get; set; }
        public string? MoviePriceName { get; set; }

        // Add these properties if they are needed
        public string? Movie { get; set; }
        public string? Sessions { get; set; }
    }
}
