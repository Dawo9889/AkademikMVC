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
    public class ResidentMappingProfiles : Profile
    {
        public ResidentMappingProfiles()
        {
            CreateMap<CreateResidentDTO, Resident>()
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
            
            
            CreateMap<Resident, FewResidentInfoDTO>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber));


            CreateMap<Resident, DetailsResidentDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ResidentDetails.Email))
                .ForMember(dest => dest.StudentCardNumber, opt => opt.MapFrom(src => src.ResidentDetails.StudentCardNumber))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ResidentDetails.PhoneNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.ResidentDetails.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.ResidentDetails.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.ResidentDetails.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ResidentDetails.PostalCode))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.ResidentDetails.Photo))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber));

        }
    }
}
