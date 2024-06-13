using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akademik.Domain.Entities
{
    public class Resident
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PESEL { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int ResidentDetailsId { get; set; }

        [ForeignKey("ResidentDetailsId")]
        public ResidentDetails? ResidentDetails { get; set; }
        public int? RoomNumber { get; set; }

        [ForeignKey("RoomNumber")]
        public Room? Room { get; set; }
    }
}
