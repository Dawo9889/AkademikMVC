using Akademik.Application.DTO.MalfunctionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.Services.MalfunctionService
{
    public interface IMalfunctionService
    {
        Task Create(CreateMalfunctionDTO createMalfunctionDTO);
    }
}
