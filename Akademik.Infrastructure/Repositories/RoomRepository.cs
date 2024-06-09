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

        public Task<Room?> UpdateRoomAvailability(int roomNumber)
        {
            throw new NotImplementedException();
        }
    }
}
