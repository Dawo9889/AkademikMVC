using Akademik.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.DTO.RoomDTO
{
    public class RoomDTO
    {
        public int RoomNumber { get; set; } = default!;
        public int NumberOfBeds { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
