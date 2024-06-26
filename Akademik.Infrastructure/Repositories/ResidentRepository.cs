﻿using Akademik.Domain.Entities;
using Akademik.Domain.Interfaces;
using Akademik.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Resident?> GetByResidentId(int Residentid)
        {
            return await _context.Residents
                .Include (r => r.ResidentDetails)
                .FirstOrDefaultAsync(r => r.Id == Residentid);
        }
        public async Task<Resident?> GetByStudentCardNumber(string studentCardNumber)
        {
            return await _context.Residents.FirstOrDefaultAsync(r => r.ResidentDetails.StudentCardNumber.ToLower() == studentCardNumber.ToLower());
        }

        public async Task<Resident?> GetDetails(int id)
        {
            return await _context.Residents
                        .Include(r => r.ResidentDetails)
                        .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Resident resident)
        {
            _context.Update(resident);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetCountResidentsInRoom(int roomNumber)
        {
            var listOfResidentsInRoom = await _context.Residents.Where(c => c.RoomNumber == roomNumber).ToListAsync();
            return listOfResidentsInRoom.Count();
        }

        public async Task RemoveResidentFromFroom(int id)
        {
            var resident = _context.Residents.FirstOrDefault(r => r.Id == id);

            if (resident != null)
            {
                resident.RoomNumber = null;

                _context.Residents.Update(resident);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Resident?> GetDetailsByEmailAsync(string email)
        {
            return await _context.Residents
                    .Include(r => r.ResidentDetails)
                    .FirstOrDefaultAsync(r => r.ResidentDetails.Email == email);
        }
        public async Task<Resident?> GetDetailsByStudentCardNumberAsync(string studentCardNumber)
        {
            return await _context.Residents
                    .Include(r => r.ResidentDetails)
                    .FirstOrDefaultAsync(r => r.ResidentDetails.StudentCardNumber.ToLower() == studentCardNumber.ToLower());
        }

        public async Task<ICollection<Resident>> GetResidentsWithoutRoom()
        {
            return await _context.Residents.Where(r => r.RoomNumber == null).ToListAsync();
        }

        public async Task RemoveUnassignedResidentDetails()
        {
            var unassignedDetails = await _context.ResidentsDetails
                .Where(rd => !_context.Residents.Any(r => r.ResidentDetailsId == rd.Id)) 
                .ToListAsync();

            
            _context.ResidentsDetails.RemoveRange(unassignedDetails);

           
            await _context.SaveChangesAsync();
        }
    }
}
