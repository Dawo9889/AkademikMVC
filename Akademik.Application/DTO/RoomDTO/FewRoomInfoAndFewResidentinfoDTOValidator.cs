using Akademik.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.DTO.RoomDTO
{
    public class FewRoomInfoAndFewResidentinfoDTOValidator : AbstractValidator<FewRoomInfoAndFewResidentinfoDTO>
    {
        public FewRoomInfoAndFewResidentinfoDTOValidator()
        {
            RuleFor(c => c.NumberOfBeds)
             .NotEmpty().WithMessage("Podaj ilość łóżek, która ma się znaleźć w pokoju")
             .Must(NumberOfBeds => NumberOfBeds >= 1 && NumberOfBeds <= 3)
             .WithMessage("Numer pokoju musi być w zakresie od 1 do 3")
             .Must((dto, numberOfBeds) => numberOfBeds >= dto.Residents.Count)
             .WithMessage("Liczba łóżek nie może być mniejsza niż liczba mieszkańców w pokoju");

            RuleFor(c => c.IsAvailable)
                .Must((dto, isAvailable) => dto.CanSetAvailability || !isAvailable)
                .WithMessage("Pokój nie może być oznaczony jako dostępny, jeśli są w nim mieszkańcy.");
        }
    }
}
