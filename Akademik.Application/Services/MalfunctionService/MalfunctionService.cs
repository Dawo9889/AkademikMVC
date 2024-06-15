using Akademik.Application.DTO.MalfunctionDTO;
using Akademik.Domain.Entities;
using Akademik.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Services.MalfunctionService
{
    public class MalfunctionService : IMalfunctionService
    {
        private readonly IMalfunctionRepository _malfunctionRepository;
        private readonly IResidentRepository _residentRepository;
        private readonly IMapper _mapper;

        public MalfunctionService(IMalfunctionRepository malfunctionRepository, IMapper mapper, IResidentRepository residentRepository)
        {
            _malfunctionRepository = malfunctionRepository;
            _mapper = mapper;
            _residentRepository = residentRepository;
        }

        public async Task Create(CreateMalfunctionDTO createMalfunctionDTO)
        {
    
            var malfunction = _mapper.Map<Malfunction>(createMalfunctionDTO);
            /*malfunction.Resident = await _residentRepository.GetByResidentId(malfunction.ResidentId);*/
            if (malfunction.PhotoData != null && malfunction.PhotoData.Length > 0)
            {
                malfunction.Photo = ProcessImage(malfunction.PhotoData);
            }
            await _malfunctionRepository.Create(malfunction);
        }




        public byte[] ProcessImage(IFormFile imageFile)
        {
            using (var image = Image.Load(imageFile.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(220, 300)); // Adjust dimensions as needed

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, new JpegEncoder { Quality = 80 }); // Adjust quality as needed
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
