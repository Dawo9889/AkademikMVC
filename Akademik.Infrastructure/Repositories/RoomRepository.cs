using Akademik.Domain.Entities;
using Akademik.Domain.Interfaces;
using Akademik.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AkademikDbContext _akademikDbContext;
        public RoomRepository(AkademikDbContext akademikDbContext)
        {
            _akademikDbContext = akademikDbContext;
        }

        public async Task Create(Room room)
        {
            _akademikDbContext.Add(room);
            await _akademikDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _akademikDbContext.Rooms.ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAllAvailableRooms()
        {
            return await _akademikDbContext.Rooms.Where(c => c.IsAvailable == true).ToListAsync();
        }

        public async Task<Room?> GetByRoomNumber(int roomNumber)
        {
            return await _akademikDbContext.Rooms.FirstOrDefaultAsync(c => c.RoomNumber == roomNumber);
        }

        public async Task UpdateRoomAvailability(int roomNumber)
        {
            int numberOfBedsInRoom = 0;
            var room = await _akademikDbContext.Rooms.FirstOrDefaultAsync(c => c.RoomNumber == roomNumber);

            if (room != null)
            {
                numberOfBedsInRoom = room.NumberOfBeds;
            }

            int numberOfResidentsInRoom = 0;
            var listOfResidentsInRoom = await _akademikDbContext.Residents.Where(c => c.RoomNumber == roomNumber).ToListAsync();
            numberOfResidentsInRoom = listOfResidentsInRoom.Count;

            bool isAvailable = (numberOfResidentsInRoom < numberOfBedsInRoom);

            if (room != null)
            {
                room.IsAvailable = isAvailable;
                _akademikDbContext.Rooms.Update(room);
                await _akademikDbContext.SaveChangesAsync();
            }
        }
    }
}
