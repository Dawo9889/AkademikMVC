using Akademik.Application.DTO.ResidentDTO;
using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Akademik.Application.Services.ResidentService
{
    public interface IResidentService
    {
        Task Create(CreateResidentDTO createResident);
        Task<IEnumerable<FewResidentInfoDTO>> GetAll();
        Task<IEnumerable<FewResidentInfoDTO>> GetResidentWithoutRoom();
        byte[] ProcessImage(IFormFile imageFile);

        Task<DetailsResidentDTO> GetDetails(int id);
        Task Delete(int id);
        Task RemoveResidentFromRoom(int id);
        Task UpdateResidentAsync(ResidentToEditDTO residentToEdit);
        Task<Resident?> GetByPESEL(string Pesel);
        Task<Resident?> GetByResidentId(int Residentid);
        Task<DetailsResidentDTO> GetDetailsByEmailAsync(string Email);
        Task<DetailsResidentDTO> GetDetailsByStudentCardNumber(string studentCardNumber);
    }
}