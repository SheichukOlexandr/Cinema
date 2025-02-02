using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва залу є обов'язковою.")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Місткість залу повинна бути більшою за 0.")]
        public int Capacity { get; set; }
    }
}
