﻿using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.DTO.RoomDTO;
using Akademik.Application.Services.RoomService;
using Akademik.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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
            var details = await _roomService.GetRoomByNumber(roomNumber);
            var viewModel = new FewRoomInfoAndFewResidentinfoDTO();
            if (details == null)
            {
                return NotFound();
            }
            var roomWithResidents = await _roomService.GetRoomWithResidents(details.RoomNumber);
            viewModel = roomWithResidents;

            //var roomToEdit = _mapper.Map<EditRoomAndResidentsInRoomDTO>(viewModel);
            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [Route("Room/Edit/{roomNumber}")]
        public async Task<IActionResult> UpdateRoom(EditRoomAndResidentsInRoomDTO roomToEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(roomToEdit);
            }
            await _roomService.UpdateRoom(roomToEdit);
            await _roomService.UpdateAbailabilityInRoom(roomToEdit.RoomNumber);
            return RedirectToAction(nameof(Index));
        }
    }
}
