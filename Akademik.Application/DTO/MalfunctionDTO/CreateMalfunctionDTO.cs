using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.DTO.MalfunctionDTO
{
    public class CreateMalfunctionDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? PhotoData { get; set; }
        public byte[]? Photo { get; set; }
        public int ResidentId { get; set; }
        public int RoomNumber { get; set; }

    }
}
