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

        public async Task<ICollection<Resident>> GetAll()
        {
            return await _context.Residents.ToListAsync();
        }

        public async Task<Resident?> GetByPESEL(string Pesel)
        {
            return await _context.Residents.FirstOrDefaultAsync(c => c.PESEL == Pesel);
        }

        public async Task<Resident?> GetByStudentCardNumber(string studentCardNumber)
        {
            return await _context.Residents.FirstOrDefaultAsync(c => c.ResidentDetails.StudentCardNumber == studentCardNumber);
        }
    }
}
