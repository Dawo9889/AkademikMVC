using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akademik.Domain.Entities
{
    public class ResidentDetails
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)] // Adjust max length as needed
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(20)] // Adjust max length as needed
        public string StudentCardNumber { get; set; } = default!;

        
        public string? PhoneNumber { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        
        public string? PostalCode { get; set; }

        public byte[]? PhotoData { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
    }
}