using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ім'я є обов'язковим.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Прізвище є обов'язковим.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Пошта є обов'язковою.")]
        [EmailAddress(ErrorMessage = "Некоректний формат пошти.")]
        public string RegisterEmail { get; set; }

        [Required(ErrorMessage = "Номер телефону є обов'язковим.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим.")]
        public string RegisterPassword { get; set; }

        [Required(ErrorMessage = "Підтвердження паролю є обов'язковим.")]
        [Compare("RegisterPassword", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }
    }
}
