namespace Akademik.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; } = default!;

        public int NumberOfBeds { get; set; } = default!;

        public bool IsAvailable { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}