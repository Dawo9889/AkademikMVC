using Akademik.Domain.Entities;
using Akademik.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Services
{
    public class ResidentService : IResidentService
    {
        private readonly IResidentRepository _residentRepository;

        public ResidentService(IResidentRepository residentRepository)
        {
            _residentRepository = residentRepository;
        }

        public async Task Create(Resident resident)
        {
            await _residentRepository.Create(resident);
        }

        public async Task<ICollection<Resident>> GetAll()
        {
           return await _residentRepository.GetAll();
        }
    }
}
