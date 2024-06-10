using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.DTO.RoomDTO;
using Akademik.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Services.RoomService
{
    public interface IRoomService
    {
        Task Create(RoomDTO roomDto);
        Task Delete(int roomNumber);
        Task <ICollection<RoomDTO>> GetAll();   
        Task <ICollection<RoomDTO>> GetAllAvailableRooms();
        Task UpdateAbailabilityInRoom(int roomNumber);
        Task<RoomDTO> GetRoomByNumber(int id);
        Task<FewRoomInfoAndFewResidentinfoDTO> GetRoomWithResidents(int roomNumber);
        Task UpdateRoom(FewRoomInfoAndFewResidentinfoDTO editRoomDto);
    }
}
