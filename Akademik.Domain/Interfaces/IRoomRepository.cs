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
        Task<IEnumerable<Room>> GetAll();
        Task<Room?> GetByRoomNumber(int roomNumber);
        Task<IEnumerable<Room>> GetAllAvailableRooms();
        Task<Room?> UpdateRoomAvailability(int roomNumber);
    }
}
