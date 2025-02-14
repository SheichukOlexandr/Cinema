using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_Cinema_app.Models
{
    public class SessionDetailsViewModel
    {
        public int SelectedSessionId { get; set; }
        public SessionDTO Session { get; set; }
        public IEnumerable<SelectListItem> AvailableSeats { get; set; }
    }
}