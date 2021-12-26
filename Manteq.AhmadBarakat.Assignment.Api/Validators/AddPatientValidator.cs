using Api.Model.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class PatientDtoValidator : AbstractValidator<PatientDto>
    {
        public PatientDtoValidator() 
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Name cannot be empty.");
        }
    }
}
