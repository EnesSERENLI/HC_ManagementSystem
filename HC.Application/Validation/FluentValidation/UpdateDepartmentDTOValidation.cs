using FluentValidation;
using HC.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Validation.FluentValidation
{
    public class UpdateDepartmentDTOValidation : AbstractValidator<UpdateDepartmentDTO>
    {
        public UpdateDepartmentDTOValidation()
        {
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage("DepartmentName cannot be empty!").MinimumLength(3).WithMessage("Department name must be at least 3 characters!");
        }
    }
}
