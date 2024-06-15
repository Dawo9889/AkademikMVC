using Akademik.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Domain.Interfaces
{
    public interface IMalfunctionRepository
    {
        Task Create(Malfunction malfunction);
        Task Delete(int id);
        Task Update(Malfunction malfunction);

        Task<IEnumerable<Malfunction?>> GetAll();

        Task<Malfunction?> GetById(int id);

        Task<IEnumerable<Malfunction?>> GetAllByResidentId (int residentId);

        Task<IEnumerable<Malfunction?>> GetAllByRoomNumber(int roomNumber);
    }
}
