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
               .NotEmpty().WithMessage("Room number is required")
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
                .NotEmpty().WithMessage("Student Card Number is required.")
                .Length(8)
                .Matches(@"^[A-Za-z]{2}[0-9]{6}$").WithMessage("Student Card Number must consist of 2 letters followed by 6 digits.")
                .Custom((value, context) =>
                {
                    var existingResident = residentRepository.GetByStudentCardNumber(value).Result;
                    if (existingResident != null)
                    {
                        context.AddFailure("Student Card Number must be unique!");
                    }
                 });


            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(c => c.RoomNumber)
             .NotEmpty().WithMessage("The RoomNumber field is required.")
             .Must(roomNumber => roomNumber >= 1 && roomNumber <= 1000)
             .WithMessage("Room bumber must be between 1 and 1000");

            RuleFor(c => c.PhotoData)
                .NotEmpty().WithMessage("Photo is required");
        }
    }
}
