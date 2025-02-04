namespace BusinessLogic.DTOs.ViewModel
{
    public class MovieSelectionViewModel
    {
        public int SelectedMovieId { get; set; }
        public List<MovieDTO> Movies { get; set; }
        public List<MoviePriceDTO> Prices { get; set; }
    }
}