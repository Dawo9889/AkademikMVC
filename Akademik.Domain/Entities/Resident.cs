using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Domain.Entities
{
    public class Resident
    {
        
        [Key]
        public string PESEL { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string EncodedName { get; private set; } = default!;

        public ResidentDetails? ResidentDetails { get; set; }


        public Room? RoomNumber { get; set; }


        public void EncodeName() => EncodedName = FirstName.ToLower().Replace(" ", "-") + LastName.ToLower().Replace(" ", "-");


    }
}
