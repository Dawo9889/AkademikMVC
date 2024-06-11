using Akademik.Application.DTO.ResidentDTO;

namespace Akademik.Application.DTO.RoomDTO
{
    public class FewRoomInfoAndFewResidentinfoDTO
    {
        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public ICollection<FewResidentInfoDTO> Residents { get; set; } = new List<FewResidentInfoDTO>();
        public bool CanSetAvailability => Residents.Count == 0;
        private int _residentsInRoom;
        public int residentsInRoom
        {
            get => Residents.Count();
            set => _residentsInRoom = value;
        }
    }
}
