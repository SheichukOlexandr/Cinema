using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Пошта є обов'язковою.")]
        [EmailAddress(ErrorMessage = "Некоректний формат пошти.")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим.")]
        public string LoginPassword { get; set; }
    }
}
