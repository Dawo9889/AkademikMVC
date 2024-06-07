using Akademik.Domain.Entities;


namespace Akademik.Domain.Interfaces
{
    public interface IResidentRepository
    {
        Task Create(Resident resident);
    }
}
