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
                 if(value == 0) { context.AddFailure("The room cannot have 0 beds."); }
             })
             .Must(NumberOfBeds => NumberOfBeds >= 1 && NumberOfBeds <= 3)
             .WithMessage("The number of beds must be between 1 and 3")
             .NotEmpty().WithMessage("This input cannot be empty")
             .Custom((value, context) =>
             {
                 var roomNumber = context.InstanceToValidate.RoomNumber;
                 var existingRoomTask = roomRepository.GetRoomWithResidents(roomNumber);
                 var existingRoom = existingRoomTask.Result;

                 if (existingRoom == null)
                 {
                     context.AddFailure("Room not found.");
                     return;
                 }

                 if (value < existingRoom.Residents.Count)
                 {
                     context.AddFailure("The number of beds cannot be less than the current number of residents in the room");
                 }
             });

            RuleFor(c => c.IsAvailable)
                .Must((dto, isAvailable) => dto.CanSetAvailability || !isAvailable)
                .WithMessage("Pokój nie może być oznaczony jako dostępny, jeśli są w nim mieszkańcy.");
        }
    }
}
