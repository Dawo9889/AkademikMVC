namespace Akademik.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int NumberOfBeds { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();
    }
}