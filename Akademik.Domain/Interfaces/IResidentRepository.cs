using Akademik.Domain.Entities;


namespace Akademik.Domain.Interfaces
{
    public interface IResidentRepository
    {
        Task Create(Resident resident);
        Task Delete(int id);
        Task<ICollection<Resident>> GetAll();
        Task<Resident?> GetByPESEL(string Pesel);
        Task<Resident?> GetByStudentCardNumber(string studentCardNumber);
        Task<Resident?> GetByResidentId(int Residentid);
        Task<Resident?> GetDetails(int id);
        Task UpdateAsync(Resident resident);
        Task<int> GetCountResidentsInRoom(int roomNumber);
    }
}
