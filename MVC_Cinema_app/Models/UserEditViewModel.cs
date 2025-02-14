using BusinessLogic.DTOs;

namespace MVC_Cinema_app.Models
{
    public class UserEditViewModel
    {
        public UserDTO User { get; set; }
        public string NewPasswordConfirmed { get; set; }
    }
}
