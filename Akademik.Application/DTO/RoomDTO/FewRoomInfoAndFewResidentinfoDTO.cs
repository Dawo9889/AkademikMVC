using Akademik.Application.DTO.ResidentDTO;

namespace Akademik.Application.DTO.RoomDTO
{
    public class FewRoomInfoAndFewResidentinfoDTO
    {
        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<FewResidentInfoDTO> Residents { get; set; } = new List<FewResidentInfoDTO>();
        public bool CanSetAvailability => Residents.Count == 0;
        public int SelectedResidentId { get; set; }
        public int countOfResidentsWithoutRoom { get; set; }
    }
}
