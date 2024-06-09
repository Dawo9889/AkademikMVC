using System.ComponentModel.DataAnnotations;

namespace Akademik.Domain.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoomNumber { get; set; } = default!;
        [Required]
        public int NumberOfBeds { get; set; } = default!;
        [Required]
        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();
    }
}