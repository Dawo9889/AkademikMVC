using Akademik.Domain.Entities;
using Akademik.Domain.Interfaces;
using Akademik.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Infrastructure.Repositories
{
    public class MalfunctionRepository : IMalfunctionRepository
    {
        private readonly AkademikDbContext _context;

        public MalfunctionRepository(AkademikDbContext context)
        {
            _context = context;
        }

        public async Task Create(Malfunction malfunction)
        {
            _context.Add(malfunction);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var malfunction = await _context.Malfunctions.FirstOrDefaultAsync(m => m.Id == id);
            if (malfunction != null)
            {
                _context.Malfunctions.Remove(malfunction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Malfunction?>> GetAll()
        {
            var malfunctions = await _context.Malfunctions.ToListAsync();
            return malfunctions;
        }

        public async Task<IEnumerable<Malfunction?>> GetAllByResidentId(int residentId)
        {
            var malfunctions = await _context.Malfunctions
                .Where(r => r.ResidentId == residentId)
                .ToListAsync();
            return malfunctions;
        }

        public async Task<IEnumerable<Malfunction?>> GetAllByRoomNumber(int roomNumber)
        {
            var malfunctions = await _context.Malfunctions
                .Where(m => m.RoomNumber == roomNumber)
                .Include(m => m.Resident) 
                .ToListAsync();

            return malfunctions;
        }

        public async Task<Malfunction?> GetById(int id)
        {
            return await _context.Malfunctions.FindAsync(id);
        }

        public Task Update(Malfunction malfunction)
        {
            throw new NotImplementedException();
        }
    }
}
