using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class SeatDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ідентифікатор кімнати є обов'язковим.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Номер місця є обов'язковим.")]
        [Range(1, int.MaxValue, ErrorMessage = "Номер місця повинен бути додатним числом.")]
        public int Number { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Додаткова вартість повинна бути невід'ємною.")]
        public decimal ExtraPrice { get; set; }

        // Дані для мапінгу:
        public string? RoomName { get; set; }
    }
}
