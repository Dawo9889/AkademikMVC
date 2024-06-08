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
    public class ResidentRepository : IResidentRepository
    {
        private readonly AkademikDbContext _context;

        public ResidentRepository(AkademikDbContext context)
        {
            _context = context;
        }

        public async Task Create(Resident resident)
        {
            _context.Residents.Add(resident);
            await _context.SaveChangesAsync();
            
        }

        public async Task Delete(int id)
        {
            var resident = await _context.Residents
                .Include(r => r.ResidentDetails) 
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resident != null)
            {
                if (resident.ResidentDetails != null)
                {
                    _context.ResidentsDetails.Remove(resident.ResidentDetails); 
                }

                _context.Residents.Remove(resident); 
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Resident>> GetAll()
        {
            return await _context.Residents.ToListAsync();
        }

        public async Task<Resident?> GetByPESEL(string Pesel)
        {
            return await _context.Residents.FirstOrDefaultAsync(r => r.PESEL == Pesel);
        }

        public async Task<Resident?> GetByStudentCardNumber(string studentCardNumber)
        {
            return await _context.Residents.FirstOrDefaultAsync(r => r.ResidentDetails.StudentCardNumber == studentCardNumber);
        }

        public async Task<Resident?> GetDetails(int id)
        {
            return await _context.Residents
                        .Include(r => r.ResidentDetails) 
                        .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
