using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.Services.RoomService;
using Akademik.Domain.Entities;
using Akademik.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg; // Adjust if you use a different format
using SixLabors.ImageSharp.Processing;

namespace Akademik.Application.Services.ResidentService
{
    public class ResidentService : IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public ResidentService(IResidentRepository residentRepository, IRoomService roomService, IMapper mapper)
        {
            _residentRepository = residentRepository;
            _roomService = roomService;
            _mapper = mapper;
        }

        public async Task Create(CreateResidentDTO createResidentDto)
        {
            var resident = _mapper.Map<Resident>(createResidentDto);

            if (resident.ResidentDetails.PhotoData != null && resident.ResidentDetails.PhotoData.Length > 0)
            {
                resident.ResidentDetails.Photo = ProcessImage(resident.ResidentDetails.PhotoData);
            }

            await _residentRepository.Create(resident);
            await UpdateRoomAvailability(resident.RoomNumber);
        }
        private async Task UpdateRoomAvailability(int? roomId)
        {
            await _roomService.UpdateAvailabilityInRoom(roomId);
        }
        public async Task Delete(int id)
        {
            var resident = await _residentRepository.GetByResidentId(id);
            if (resident != null)
            {
                await _residentRepository.Delete(id);
                await UpdateRoomAvailability(resident.RoomNumber);
            }
        }

        public async Task<IEnumerable<FewResidentInfoDTO>> GetAll()
        {
            var residents = await _residentRepository.GetAll();
            return _mapper.Map<IEnumerable<FewResidentInfoDTO>>(residents);
        }

        public async Task<DetailsResidentDTO> GetDetails(int id)
        {
            var details = await _residentRepository.GetDetails(id);
            return _mapper.Map<DetailsResidentDTO>(details);
        }

        public async Task UpdateResidentAsync(ResidentToEditDTO residentToEdit)
        {
            var resident = _residentRepository.GetByResidentId(residentToEdit.Id).Result;
            if (resident == null)
            {
                return;
            }
            resident = _mapper.Map(residentToEdit, resident);
            if (resident.ResidentDetails.PhotoData != null && resident.ResidentDetails.PhotoData.Length > 0)
            {
                resident.ResidentDetails.Photo = ProcessImage(resident.ResidentDetails.PhotoData);
            }
            await _residentRepository.UpdateAsync(resident);
        }

        public async Task<Resident?> GetByPESEL(string Pesel)
        {
            return await _residentRepository.GetByPESEL(Pesel);
        }

        public async Task<Resident?> GetByResidentId(int Residentid)
        {
            return await _residentRepository.GetByResidentId(Residentid);
        }




        public byte[] ProcessImage(IFormFile imageFile)
        {
            using (var image = Image.Load(imageFile.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(300, 300)); // Adjust dimensions as needed

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, new JpegEncoder { Quality = 80 }); // Adjust quality as needed
                    return memoryStream.ToArray();
                }
            }
        }


        public async Task RemoveResidentFromRoom(int id)
        {
            var resident = await _residentRepository.GetByResidentId(id);
            if (resident != null)
            {
                await _residentRepository.RemoveResidentFromFroom(id);
                await UpdateRoomAvailability(resident.RoomNumber);
            }
        }

        public async Task<IEnumerable<FewResidentInfoDTO>> GetResidentWithoutRoom()
        {
            var residents = await _residentRepository.GetResidentsWithoutRoom();
            return _mapper.Map<IEnumerable<FewResidentInfoDTO>>(residents);
        }
    }
}
