using Akademik.Application.DTO.ResidentDTO;
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

        public async Task UpdateAvailabilityInRoom(int? roomNumber)
        {
            await _roomRepository.UpdateRoomAvailability(roomNumber);
        }
        public async Task<FewRoomInfoAndFewResidentinfoDTO> GetRoomWithResidents(int roomNumber)
        {
            var room = await _roomRepository.GetRoomWithResidents(roomNumber);
            return _mapper.Map<FewRoomInfoAndFewResidentinfoDTO>(room);
        }

        public async Task Delete(int roomNumber)
        {
            var room = await _roomRepository.GetByRoomNumber(roomNumber);
            if (room != null)
            {
                await _roomRepository.Delete(roomNumber);
            }
        }

        public async Task<RoomDTO> GetRoomByNumber(int id)
        {
            var room = await _roomRepository.GetByRoomNumber(id);
            return _mapper.Map<RoomDTO>(room);
        }

        public async Task UpdateRoom(FewRoomInfoAndFewResidentinfoDTO roomToEdit)
        {
            var existingRoom = await _roomRepository.GetByRoomNumber(roomToEdit.RoomNumber);

            if (existingRoom == null)
            {
                throw new ArgumentException($"Room with RoomNumber {roomToEdit.RoomNumber} not found.");
            }

            existingRoom.NumberOfBeds = roomToEdit.NumberOfBeds;
            existingRoom.IsAvailable = roomToEdit.IsAvailable;

            await _roomRepository.Update(existingRoom);
        }
        public async Task AddResidentToRoom(int roomNumber, int residentId)
        {
            await _roomRepository.AddResidentToRoom(roomNumber, residentId);
        }
    }
}
