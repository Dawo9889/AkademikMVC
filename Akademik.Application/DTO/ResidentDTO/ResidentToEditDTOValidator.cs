using Akademik.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.DTO.ResidentDTO
{
    public class ResidentToEditDTOValidator : AbstractValidator<ResidentToEditDTO>
    {
        public ResidentToEditDTOValidator(IResidentRepository residentRepository)
        {
            RuleFor(c => c.PESEL)
               .NotEmpty().WithMessage("Numer pokoju jest wymagany")
               .Length(11)
               .Must(value => long.TryParse(value, out _)).WithMessage("PESEL must contains 11 digits.");



            RuleFor(c => c.StudentCardNumber)
                .NotEmpty()
                .Length(8);
               


            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(c => c.RoomNumber)
             .NotEmpty().WithMessage("The RoomNumber field is required.")
             .Must(roomNumber => roomNumber >= 1 && roomNumber <= 1000)
             .WithMessage("Numer pokoju musi być w zakresie od 1 do 1000");

            RuleFor(c => c.PhotoData)
                .NotEmpty().WithMessage("Photo is required");
        }
    }
}
