﻿using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.Services.ResidentService;
using Akademik.Application.Services.RoomService;
using Akademik.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AkademikMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ResidentController : Controller
    {

        private readonly IResidentService _residentService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public ResidentController(IResidentService residentService, IRoomService roomService, IMapper mapper)
        {
            _residentService = residentService;
            _roomService = roomService;
            _mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> List()
        {
            var residents = await _residentService.GetAll();

            return View(residents);
        }

        [HttpGet]
        [Route("Resident/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var details = await _residentService.GetDetails(id);

            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }
        [HttpGet]
        [Route("Resident/DetailsByPesel/{PESEL?}")]
        public async Task<IActionResult> DetailsPesel(string PESEL)
        {
            if(PESEL == string.Empty)
            {
                return RedirectToAction("ResidentNotFound");
            }
            var resident = await _residentService.GetByPESEL(PESEL);
            if(resident == null)
            {
                return RedirectToAction("ResidentNotFound");
            }
            var details = await _residentService.GetDetails(resident.Id);

            if (details == null)
            {
                return RedirectToAction("ResidentNotFound");
            }

            return View(details);
        }
        [HttpGet]
        [Route("Resident/ResidentNotFound")]
        public IActionResult ResidentNotFound()
        {
            return View();
        }
        [HttpGet]
        [Route("Resident/Create")]
        public async Task<IActionResult> Create()
        {
            var availableRooms = await _roomService.GetAllAvailableRooms();
            ViewBag.AvailableRooms = availableRooms;

            return View();
        }

        [HttpPost]
        [Route("Resident/Create")]
        public async Task<IActionResult> Create(CreateResidentDTO createResident)
        {
            if (ModelState.IsValid)
            {
                await _residentService.Create(createResident);
                return RedirectToAction(nameof(List));
            }
            var availableRooms = await _roomService.GetAllAvailableRooms();
            ViewBag.AvailableRooms = availableRooms;
            return View(createResident);
        }


        [HttpGet]
        [Route("Resident/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resident = await _residentService.GetDetails(id);
            if (resident == null)
            {
                return NotFound();
            }

            return View(resident);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Resident/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resident = await _residentService.GetDetails(id);
            if (resident == null)
            {
                return NotFound();
            }

            await _residentService.Delete(id);
            await _roomService.UpdateAvailabilityInRoom(resident.RoomNumber);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        [Route("Resident/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var details = await _residentService.GetDetails(id);
            var residentToEdit = _mapper.Map<ResidentToEditDTO>(details);

            var availableRooms = await _roomService.GetAllAvailableRooms();
            ViewBag.AvailableRooms = availableRooms;

            if (residentToEdit == null)
            {
                return NotFound();
            }
            TempData["OldRoomNumber"] = residentToEdit.RoomNumber;
            return View(residentToEdit);
        }

        [HttpPost]
        [Route("Resident/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, ResidentToEditDTO residentToEdit)
        {
            if (!ModelState.IsValid)
            {
                var availableRooms = await _roomService.GetAllAvailableRooms();
                ViewBag.AvailableRooms = availableRooms;
                return View(residentToEdit);
            }
            var oldResident = await _residentService.GetByResidentId(id);

            if (oldResident == null)
            {
                return View(residentToEdit);
            }

            if (oldResident.PESEL != residentToEdit.PESEL)
            {
                // Wykorzystanie funkcji GetByPESEL z serwisu do sprawdzenia unikalności
                var existingResident = await _residentService.GetByPESEL(residentToEdit.PESEL);

                if (existingResident != null)
                {
                    var availableRooms = await _roomService.GetAllAvailableRooms();
                    ViewBag.AvailableRooms = availableRooms;
                    ModelState.AddModelError("PESEL", "Taki PESEL już jest w bazie");
                    return View(residentToEdit);
                }
            }
            if (oldResident.ResidentDetails.StudentCardNumber != residentToEdit.StudentCardNumber)
            {
                
                var existingResident = await _residentService.GetDetailsByStudentCardNumber(residentToEdit.StudentCardNumber);

                if (existingResident != null)
                {
                    var availableRooms = await _roomService.GetAllAvailableRooms();
                    ViewBag.AvailableRooms = availableRooms;
                    ModelState.AddModelError("StudentCardNumber", "Taka karta studencka już jest w bazie");
                    return View(residentToEdit);
                }
            }
            var oldRoomNumber = (int)TempData["OldRoomNumber"];

            await _residentService.UpdateResidentAsync(residentToEdit);
            await _roomService.UpdateAvailabilityInRoom(oldRoomNumber);
            await _roomService.UpdateAvailabilityInRoom(residentToEdit.RoomNumber);

            TempData.Remove("OldRoomNumber");
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        [Route("Resident/RemoveResidentFromRoom/{id}")]
        public async Task<IActionResult> RemoveResidentFromRoom(int id)
        {
            var resident = await _residentService.GetByResidentId(id);
            var residentToEdit = _mapper.Map<FewResidentInfoDTO>(resident);

            if (residentToEdit == null)
            {
                return NotFound();
            }
            return View(residentToEdit);
        }
        [HttpPost]
        [Route("Resident/RemoveResidentFromRoom/{id}")]
        public async Task<IActionResult> RemoveResidentFromRoomConfirm(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var resident = await _residentService.GetDetails(id);
            if (resident == null)
            {
                return NotFound();
            }
            await _residentService.RemoveResidentFromRoom(id);
            await _roomService.UpdateAvailabilityInRoom(resident.RoomNumber);
            return RedirectToAction("Index", "Room");
        }

    }
}
