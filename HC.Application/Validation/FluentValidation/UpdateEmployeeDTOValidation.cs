using FluentValidation;
using HC.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Validation.FluentValidation
{
    public class UpdateEmployeeDTOValidation : AbstractValidator<UpdateEmployeeDTO>
    {
        public UpdateEmployeeDTOValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName cannot be empty!");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName cannot be empty!");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty!");
            RuleFor(x => x.Address).MinimumLength(3).MaximumLength(500).WithMessage("Character min :3 , max : 500");
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("DepartmentId cannot be empty!");
        }
    }
}
