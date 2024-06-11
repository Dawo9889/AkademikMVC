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
        public FewRoomInfoAndFewResidentinfoDTOValidator(IRoomRepository roomRepository)
        {
            RuleFor(c => c.NumberOfBeds)
             .Custom((value, context) =>
             {
                 if(value == 0) { context.AddFailure("Pokój nie może mieć 0 łóżek."); }
             })
             .Must(NumberOfBeds => NumberOfBeds >= 1 && NumberOfBeds <= 3)
             .WithMessage("Numer pokoju musi być w zakresie od 1 do 3")
             .NotEmpty().WithMessage("Pole nie może być puste")
             .Custom((value, context) =>
             {
                 var roomNumber = context.InstanceToValidate.RoomNumber;
                 var existingRoomTask = roomRepository.GetRoomWithResidents(roomNumber);
                 var existingRoom = existingRoomTask.Result;

                 if (existingRoom == null)
                 {
                     context.AddFailure("Pokój nie został znaleziony.");
                     return;
                 }

                 if (value < existingRoom.Residents.Count)
                 {
                     context.AddFailure("Liczba łóżek nie może być mniejsza niż obecna liczba mieszkańców w pokoju");
                 }
             });

            RuleFor(c => c.IsAvailable)
                .Must((dto, isAvailable) => dto.CanSetAvailability || !isAvailable)
                .WithMessage("Pokój nie może być oznaczony jako dostępny, jeśli są w nim mieszkańcy.");
        }
    }
}
