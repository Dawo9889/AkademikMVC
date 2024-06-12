using Akademik.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task Create(Room room);
        Task Delete(int roomNumber);
        Task Update(Room room);
        Task AddResidentToRoom(int roomNumber, int residentId);
        Task<IEnumerable<Room>> GetAll();
        Task<Room?> GetByRoomNumber(int roomNumber);
        Task<IEnumerable<Room>> GetAllAvailableRooms();
        Task UpdateRoomAvailability(int? roomNumber);
        Task<Room> GetRoomWithResidents(int roomNumber);
    }
}
