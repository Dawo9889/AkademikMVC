using Akademik.Application.DTO;
using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Akademik.Application.Services
{
    public interface IResidentService
    {
        Task Create(ResidentDTO resident);
        Task<ICollection<Resident>> GetAll();
        byte[] ProcessImage(IFormFile imageFile);
    }
}