using Akademik.Application.DTO.ResidentDTO;
using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Akademik.Application.Services.ResidentService
{
    public interface IResidentService
    {
        Task Create(ResidentDTO resident);
        Task<ICollection<Resident>> GetAll();
        byte[] ProcessImage(IFormFile imageFile);
    }
}