using Akademik.Application.Services;
using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AkademikMVC.Controllers
{
    
    public class ResidentController : Controller 
    {

        private readonly IResidentService _residentService;

        public ResidentController(IResidentService residentService)
        {
            _residentService = residentService;
        }

        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        [Route("/Resident")]
        public async Task<IActionResult> Create(Resident resident)
        {
            await _residentService.Create(resident);
            return RedirectToAction(nameof(Create));
        }



    }
}
