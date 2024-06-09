using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.DTO.RoomDTO;
using Akademik.Application.Services.RoomService;
using Microsoft.AspNetCore.Mvc;

namespace AkademikMVC.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoomDTO roomDTO)
        {
            if (ModelState.IsValid)
            {
                await _roomService.Create(roomDTO);
                return RedirectToAction(nameof(Create));
            }
            return View(roomDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAll();

            return View(rooms);
        }
    }
}
