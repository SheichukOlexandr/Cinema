using BusinessLogic.DTOs;

namespace MVC_Cinema_app.Models
{
    public class MovieViewModel
    {
        public MovieDTO Movie { get; set; }
        public IEnumerable<SessionDTO> Sessions { get; set; }
    }
}