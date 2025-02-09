namespace BusinessLogic.DTOs
{
    public class ReservationStatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Constants for predefined reservation statuses
        public const string Created = "Створено";
        public const string Confirmed = "Підтверджено";
        public const string Completed = "Завершено";
        public const string Cancelled = "Скасовано";
    }
}