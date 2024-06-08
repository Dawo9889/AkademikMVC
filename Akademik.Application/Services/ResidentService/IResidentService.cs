﻿using Akademik.Application.DTO.ResidentDTO;
using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Akademik.Application.Services.ResidentService
{
    public interface IResidentService
    {
        Task Create(CreateResidentDTO createResident);
        Task<IEnumerable<FewResidentInfoDTO>> GetAll();
        byte[] ProcessImage(IFormFile imageFile);

        Task<DetailsResidentDTO> GetDetails(int id);
        Task Delete(int id);
    }
}