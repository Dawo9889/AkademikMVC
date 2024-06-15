using Akademik.Application.DTO.MalfunctionDTO;
using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.Services.MalfunctionService;
using Akademik.Application.Services.ResidentService;
using Akademik.Application.Services.RoomService;
using Akademik.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AkademikMVC.Controllers
{
    public class MalfunctionController : Controller
    {
        private readonly IMalfunctionService _malfunctionService;
        private readonly IResidentService _residentService;
        private readonly IRoomService _roomService;

        public MalfunctionController(IMalfunctionService malfunctionService, IResidentService residentService, IRoomService roomService)
        {
            _malfunctionService = malfunctionService;
            _residentService = residentService;
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if(!User.Identity.IsAuthenticated)
            {
                var roomNumber = 99999;
            }
            var resident = await _residentService.GetDetailsByStudentCardNumber(User.Identity.Name);
            if (resident == null)
            {
                var roomNumber = resident.RoomNumber;
                ViewBag.RoomNumber = roomNumber;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMalfunctionDTO malfunctionDto)
        {
            if (!ModelState.IsValid)
            {
                return View(malfunctionDto);
            }
            
            var resident = await _residentService.GetDetailsByStudentCardNumber(User.Identity.Name);
            if (resident != null)
            {
                malfunctionDto.ResidentId = int.Parse(resident.Id);
                malfunctionDto.RoomNumber = 101;
            }
            else
            {
                malfunctionDto.ResidentId = 1;
                malfunctionDto.RoomNumber = 101;
            }
            
            await _malfunctionService.Create(malfunctionDto);
            return Ok();
        }
    }
}
