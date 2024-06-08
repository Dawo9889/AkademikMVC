using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using Akademik.Application.DTO.ResidentDTO;
using Akademik.Application.Services.ResidentService;
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
        public async Task<IActionResult> List()
        {
            var residents = await _residentService.GetAll();

            return View(residents);
        }

        [HttpGet]
        [Route("{id}")]
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
        public IActionResult Create()
        {
           return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateResidentDTO createResident)
        {
            if (ModelState.IsValid)
            {
                await _residentService.Create(createResident);
                return RedirectToAction(nameof(Create));
            }
            return View(createResident);
        }


        

    }
}
