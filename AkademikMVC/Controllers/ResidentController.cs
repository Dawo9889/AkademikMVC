using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.Services.ResidentService;
using Akademik.Application.Services.RoomService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AkademikMVC.Controllers
{

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
            var oldRoomNumber = (int)TempData["OldRoomNumber"];
            //sprawdzenie czy nowy pesel juz nie istnieje
            if (await _residentService.GetByPESEL(residentToEdit.PESEL) != null)
            {
                var availableRooms = await _roomService.GetAllAvailableRooms();
                ViewBag.AvailableRooms = availableRooms;
                ModelState.AddModelError("PESEL", "Taki PESEL już jest w bazie");
                return View(residentToEdit);
            }
            await _residentService.UpdateResidentAsync(residentToEdit);
            await _roomService.UpdateAbailabilityInRoom(oldRoomNumber);
            await _roomService.UpdateAbailabilityInRoom(residentToEdit.RoomNumber);

            TempData.Remove("OldRoomNumber");
            return RedirectToAction(nameof(List));
        }


    }
}
