using Akademik.Application.Services.ResidentService;
using Akademik.Application.Services.RoomService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkademikMVC.Controllers
{
    [Authorize]
    public class UserInfoController : Controller
    {
        private readonly IResidentService _residentService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public UserInfoController(IResidentService residentService, IRoomService roomService, IMapper mapper)
        {
            _residentService = residentService;
            _roomService = roomService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var details = await _residentService.GetDetailsByEmailAsync(User.Identity.Name);

            if (details == null)
            {
                View();
            }

            return View(details);
        }


    }
}
