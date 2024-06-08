using Akademik.Application.Services;
using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using Akademik.Application.DTO;
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
        public async Task<IActionResult> Create(ResidentDTO resident)
        {
            if (ModelState.IsValid)
            {
                await _residentService.Create(resident);
                return RedirectToAction(nameof(Create));
            }
            return View(resident);
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var residents = await _residentService.GetAll();

            return View(residents);
        }

    }
}
