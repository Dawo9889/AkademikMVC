using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Akademik.Application.Services
{
    public interface IResidentService
    {
        Task Create(Resident resident);
        Task<ICollection<Resident>> GetAll();
        byte[] ProcessImage(IFormFile imageFile);
    }
}