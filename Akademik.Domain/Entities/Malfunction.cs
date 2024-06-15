using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akademik.Domain.Entities
{
    public class Malfunction
    {
        public int Id { get; set; } // Klucz główny
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [NotMapped] // This property is not mapped to a database column
        public IFormFile? PhotoData { get; set; }
        public byte[]? Photo { get; set; } // Zdjęcie usterki (opcjonalne)
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
        public MalfunctionStatus Status { get; set; } = MalfunctionStatus.New; // Stan usterki (np. Nowa, W trakcie naprawy, Naprawiona)
        public int ResidentId { get; set; } // Klucz obcy do Resident
        public int RoomNumber { get; set; } // Klucz obcy do Room

        // Relacja z mieszkańcem (jedna usterka do jednego mieszkańca)
        [ForeignKey("ResidentId")]
        public Resident Resident { get; set; }

        // Relacja z pokojem (jedna usterka do jednego pokoju)
        [ForeignKey("RoomNumber")]
        public Room Room { get; set; }
    }
    public enum MalfunctionStatus
    {
        New,
        InProgress,
        Fixed
    }
}
