using Akademik.Application.DTO.RoomDTO;
using Akademik.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Services.RoomService
{
    public interface IRoomService
    {
        Task Create(RoomDTO roomDto);
        Task <ICollection<Room>> GetAll();   
    }
}
