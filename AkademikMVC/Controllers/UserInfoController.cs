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

        public UserInfoController(IResidentService residentService)
        {
            _residentService = residentService;
        }

        public async Task<IActionResult> Index()
        {
            var details = await _residentService.GetDetailsByStudentCardNumber(User.Identity.Name);

            if (details == null)
            {
                View();
            }

            return View(details);
        }


    }
}
