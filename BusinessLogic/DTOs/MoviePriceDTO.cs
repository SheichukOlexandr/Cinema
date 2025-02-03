namespace BusinessLogic.DTOs
{
    public class MoviePriceDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }

        // data to map:
        public string? MovieName { get; set; }
    }
}

