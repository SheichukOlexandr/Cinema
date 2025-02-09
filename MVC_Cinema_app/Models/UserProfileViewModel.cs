using BusinessLogic.DTOs;

namespace MVC_Cinema_app.Models
{
    public class UserProfileViewModel
    {
        public UserDTO User { get; set; }
        public IEnumerable<ReservationDTO> Reservations { get; set; }
    }
}
