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

        [HttpGet("{residentId}/{roomNumber}")]
        public async Task<IActionResult> Create(int residentId, int roomNumber)
        {

            var malfunctionDto = new CreateMalfunctionDTO
            {
                ResidentId = residentId,
                RoomNumber = roomNumber
            };

            return View(malfunctionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMalfunctionDTO malfunctionDto)
        {
            if (!ModelState.IsValid)
            {
                return View(malfunctionDto);
            }
           
            await _malfunctionService.Create(malfunctionDto);
            return RedirectToAction("Index", "UserInfo");
        }
    }
}
