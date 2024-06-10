using Akademik.Application.DTO.ResidentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.DTO.RoomDTO
{
    public class EditRoomAndResidentsInRoomDTO
    {
        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public ICollection<FewResidentInfoDTO> Residents { get; set; } = new List<FewResidentInfoDTO>();

        public bool CanSetAvailability => Residents.Count == 0;
    }
}
