using Akademik.Application.DTO.RoomDTO;
using Akademik.Domain.Entities;
using Akademik.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task Create(RoomDTO roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            await _roomRepository.Create(room);
        }

        public async Task<ICollection<RoomDTO>> GetAll()
        {
            var rooms = await _roomRepository.GetAll();
            return _mapper.Map<ICollection<RoomDTO>>(rooms);
        }

        public async Task<ICollection<RoomDTO>> GetAllAvailableRooms()
        {
            var rooms = await _roomRepository.GetAllAvailableRooms();
            return _mapper.Map<ICollection<RoomDTO>>(rooms);
        }
    }
}
