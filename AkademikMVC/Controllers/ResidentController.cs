using Akademik.Application.Services;
using Akademik.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
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
        
        public async Task<IActionResult> Create(Resident resident)
        {
            if (ModelState.IsValid)
            {
                if (resident.ResidentDetails.PhotoData != null && resident.ResidentDetails.PhotoData.Length > 0)
                {
                    using (var image = Image.Load(resident.ResidentDetails.PhotoData.OpenReadStream()))
                    {
                        //Zmiana rozmiaru obrazu
                        image.Mutate(x => x.Resize(400, 300));

                        // Kompresuj obraz
                        using (var memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, new JpegEncoder { Quality = 80 }); 

                            resident.ResidentDetails.Photo = memoryStream.ToArray();
                        }
                    }
                }
                await _residentService.Create(resident);
                return RedirectToAction(nameof(Create));
            }
            return View(resident);
        }
      
    }
}
