using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва залу обов'язкова")]
        [StringLength(50, ErrorMessage = "Назва залу не може бути довшою за 50 символів")]
        public string Name { get; set; }

        [Range(1, 500, ErrorMessage = "Кількість місць повинна бути від 1 до 500")]
        public int Capacity { get; set; }
    }
}
