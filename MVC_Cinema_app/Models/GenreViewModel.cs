using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_Cinema_app.Models
{
    public class GenreViewModel
    {
        public int SelectedGenreId { get; set; }
        public List<SelectListItem> GenreOptions { get; set; }
        public List<MovieViewModel> Movies { get; set; }
    }
}