using FluentValidation;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators
{
    public class DepartmentForManipulationDtoValidator : AbstractValidator<DepartmentForManipulationDto>
    {
        public DepartmentForManipulationDtoValidator()
        {
            RuleFor(d => d.Name).NotEmpty().MaximumLength(10).WithMessage("Department can not be empty");
            RuleFor(d => d.Location).NotEmpty().MaximumLength(10).WithMessage("Location can not be empty");
            
        }
    }
}
