using Akademik.Application.DTO.MalfunctionDTO;
using Akademik.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Mappings
{
    public class MalfunctionMappingProfiles : Profile
    {
        public MalfunctionMappingProfiles()
        {
            CreateMap<CreateMalfunctionDTO, Malfunction>();
        }
    }
}
