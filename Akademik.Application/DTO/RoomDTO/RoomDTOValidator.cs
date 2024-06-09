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
                .NotEmpty().WithMessage("Podaj numer pokoju ktory chcesz dodac!")
                .LessThan(1000).WithMessage("Numer pokoju musi byc mniejszy od 1000!")
                .GreaterThan(0).WithMessage("Numer pokoju musi byc wiekszy od 0!")
                .Custom((value, context) =>
                {
                    var existingRoom = roomRepository.GetByRoomNumber(value).Result;
                    if (existingRoom != null)
                    {
                        context.AddFailure("pokoj juz istnieje");
                    }
                });

        }
    }
}
