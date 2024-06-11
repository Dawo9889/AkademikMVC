using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.DTO.RoomDTO;
using Akademik.Application.Services.RoomService;
using Akademik.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AkademikMVC.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoomDTO roomDto)
        {
            if (ModelState.IsValid)
            {
                await _roomService.Create(roomDto);
                return RedirectToAction(nameof(Index));
            }
            return View(roomDto);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAll();
            var viewModel = new List<FewRoomInfoAndFewResidentinfoDTO>();

            foreach (var room in rooms)
            {
                var roomWithResidents = await _roomService.GetRoomWithResidents(room.RoomNumber);
                viewModel.Add(roomWithResidents);
            }

            return View(viewModel);
        }

        [HttpGet]
        [Route("Room/Delete/{RoomNumber}")]
        public async Task<IActionResult> Delete(int roomNumber)
        {
            if (roomNumber == null)
            {
                return NotFound();
            }
            var room = await _roomService.GetRoomByNumber(roomNumber);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Room/Delete/{RoomNumber}")]
        public async Task<IActionResult> DeleteConfirmed(int roomNumber)
        {
            if (roomNumber == null)
            {
                return NotFound();
            }
            var room = await _roomService.GetRoomByNumber(roomNumber);
            if (room == null)
            {
                return NotFound();
            }
            await _roomService.Delete(roomNumber);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("Room/Edit/{roomNumber}")]
        public async Task<IActionResult> Edit(int roomNumber)
        {
            var roomWithResidents = await _roomService.GetRoomWithResidents(roomNumber);
            if (roomWithResidents == null)
            {
                return NotFound();
            }

            return View(roomWithResidents);
        }

        [HttpPost, ActionName("Edit")]
        [Route("Room/Edit/{roomNumber}")]
        public async Task<IActionResult> UpdateRoom(FewRoomInfoAndFewResidentinfoDTO roomToEdit)
        {
            if (!ModelState.IsValid)
            {
                var newRoom = await _roomService.GetRoomWithResidents(roomToEdit.RoomNumber);
                return View(newRoom);
            }
            var room = await _roomService.GetRoomWithResidents(roomToEdit.RoomNumber);

            foreach (var residentDto in roomToEdit.Residents)
            {
                var existingResident = room.Residents.FirstOrDefault(r => r.Id == residentDto.Id);
                if (existingResident != null)
                {
                    _mapper.Map(residentDto, existingResident);
                }
            }
            await _roomService.UpdateRoom(roomToEdit);
            await _roomService.UpdateAbailabilityInRoom(roomToEdit.RoomNumber);

            return RedirectToAction(nameof(Index));
        }
    }
}
