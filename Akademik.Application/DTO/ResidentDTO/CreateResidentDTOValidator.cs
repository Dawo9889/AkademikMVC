using Akademik.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akademik.Application.DTO.ResidentDTO
{
    public class CreateResidentDTOValidator : AbstractValidator<CreateResidentDTO>
    {
        public CreateResidentDTOValidator(IResidentRepository residentRepository)
        {
            RuleFor(c => c.PESEL)
               .NotEmpty().WithMessage("Numer pokoju jest wymagany")
               .Length(11)
               .Must(value => long.TryParse(value, out _)).WithMessage("PESEL must contains 11 digits.")
               .Custom((value, context) =>
               {
                   var existingResident = residentRepository.GetByPESEL(value).Result;
                   if (existingResident != null)
                   {
                       context.AddFailure("Resident with such a PESEL already exists");
                   }
               });


            RuleFor(c => c.StudentCardNumber)
                .NotEmpty()
                .Length(8)
                .Custom((value, context) =>
                {
                    var existingResident = residentRepository.GetByStudentCardNumber(value).Result;
                    if (existingResident != null)
                    {
                        context.AddFailure("Student Card Number must be unique!");
                    }
                });
            ;

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(c => c.RoomId)
             .NotEmpty().WithMessage("The RoomId field is required.")

             .Must(roomId => roomId >= 1 && roomId <= 1000)
             .WithMessage("Numer pokoju musi być w zakresie od 1 do 1000");
        }
    }
}
