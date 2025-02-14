using BusinessLogic.DTOs;

namespace MVC_Cinema_app.Models
{
    public class MovieDetailsViewModel
    {
        public MovieDTO Movie { get; set; }
        public IEnumerable<SessionDetailsViewModel> SessionDetails { get; set; }
    }
}