using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_Cinema_app.Models
{
    public class HomeViewModel
    {
        public List<SelectListItem> GenreOptions { get; set; }
        public List<SelectListItem> AgeOptions { get; set; }
        public List<MovieDetailsViewModel> Movies { get; set; }
        public List<MovieDTO> NewMovies { get; set; }
    }
}