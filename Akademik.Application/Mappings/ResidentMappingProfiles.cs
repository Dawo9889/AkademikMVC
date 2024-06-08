using Akademik.Application.DTO.ResidentDTO;
using Akademik.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Mappings
{
    public class ResidentMappingProfiles :Profile
    {
        public ResidentMappingProfiles()
        {
            CreateMap<ResidentDTO, Resident>()
                .ForMember(e => e.ResidentDetails, options => options.MapFrom(src => new ResidentDetails()
                {
                    Email = src.Email,
                    StudentCardNumber = src.StudentCardNumber,
                    PhoneNumber = src.PhoneNumber,
                    Street = src.Street,
                    City = src.City,
                    Country = src.Country,
                    PostalCode = src.PostalCode,
                    PhotoData = src.PhotoData,
                }));
                
        }
    }
}
