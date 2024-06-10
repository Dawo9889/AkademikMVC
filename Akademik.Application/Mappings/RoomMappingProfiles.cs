using Akademik.Application.DTO.ResidentDTO;
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
            CreateMap<RoomDTO, Room>();
            CreateMap<Room, RoomDTO>();
            CreateMap<Resident, FewResidentInfoDTO>();
            CreateMap<Room, FewRoomInfoAndFewResidentinfoDTO>()
                .ForMember(dest => dest.Residents, opt => opt.MapFrom(src => src.Residents));
            CreateMap<FewRoomInfoAndFewResidentinfoDTO, Room>();
            CreateMap<RoomDTO,EditRoomAndResidentsInRoomDTO>();
            CreateMap<EditRoomAndResidentsInRoomDTO, Room>();
        }
    }
}
