using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
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
        [StringLength(8)] // Adjust max length as needed
        public string StudentCardNumber { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }

        [NotMapped] // This property is not mapped to a database column
        public IFormFile? PhotoData { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[]? Photo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
    }
}