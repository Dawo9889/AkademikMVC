using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Domain.Entities
{
    public class Resident
    {
        [Key]
        public int Id { get; set; }

        [Required]
        
        public string PESEL { get; set; } = default!;

        [Required]
        public string FirstName { get; set; } = default!;

        [Required]
        public string LastName { get; set; } = default!;

        public int ResidentDetailsId { get; set; } // Dodanie właściwości ResidentDetailsId

        [ForeignKey("ResidentDetailsId")] // Określenie klucza obcego
        public ResidentDetails? ResidentDetails { get; set; } // Właściwość nawigacyjna


        public int? RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        
    }
}
