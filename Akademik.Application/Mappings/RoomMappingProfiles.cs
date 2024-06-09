using Akademik.Application.DTO.RoomDTO;
using Akademik.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Mappings
{
    public class RoomMappingProfiles : Profile
    {
        public RoomMappingProfiles()
        {
            CreateMap<RoomDTO, Room>()
                .ForMember(c => c.Id, option => option.Ignore());
        }
    }
}
