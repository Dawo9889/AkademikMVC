using Akademik.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.DTO.RoomDTO
{
    public class RoomDTOValidator : AbstractValidator<RoomDTO>
    {
        public RoomDTOValidator(IRoomRepository roomRepository)
        {
            RuleFor(c => c.RoomNumber)
                .NotEmpty().WithMessage("This field cannot be empty")
                .Must(RoomNumber => RoomNumber >= 1 && RoomNumber <= 1000)
                .WithMessage("The number of beds must be between 1 and 1000")
                .Custom((value, context) =>
                {
                    var existingRoom = roomRepository.GetByRoomNumber(value).Result;
                    if (existingRoom != null)
                    {
                        context.AddFailure("This room is exists");
                    }
                });

        }
    }
}
