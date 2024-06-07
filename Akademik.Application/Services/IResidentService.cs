using Akademik.Domain.Entities;

namespace Akademik.Application.Services
{
    public interface IResidentService
    {
        Task Create(Resident resident);
       
    }
}