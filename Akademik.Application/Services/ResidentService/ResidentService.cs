﻿using Akademik.Domain.Entities;
using Akademik.Domain.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg; // Adjust if you use a different format
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Akademik.Application.DTO.ResidentDTO;

namespace Akademik.Application.Services.ResidentService
{
    public class ResidentService : IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IMapper _mapper;

        public ResidentService(IResidentRepository residentRepository, IMapper mapper)
        {
            _residentRepository = residentRepository;
            _mapper = mapper;
        }

        public async Task Create(ResidentDTO residentDto)
        {
            var resident = _mapper.Map<Resident>(residentDto);

            if (resident.ResidentDetails.PhotoData != null && resident.ResidentDetails.PhotoData.Length > 0)
            {
                resident.ResidentDetails.Photo = ProcessImage(resident.ResidentDetails.PhotoData);
            }

            await _residentRepository.Create(resident);
        }

        public async Task<ICollection<Resident>> GetAll()
        {
            return await _residentRepository.GetAll();
        }

        public byte[] ProcessImage(IFormFile imageFile)
        {
            using (var image = Image.Load(imageFile.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(400, 300)); // Adjust dimensions as needed

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, new JpegEncoder { Quality = 80 }); // Adjust quality as needed
                    return memoryStream.ToArray();
                }
            }
        }
    }
}