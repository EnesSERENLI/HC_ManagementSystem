using FluentValidation;
using HC.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Validation.FluentValidation
{
    public class UpdateRoleDTOValidation:AbstractValidator<UpdateRoleDTO>
    {
        public UpdateRoleDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("RoleName cannot be empty!").MinimumLength(3).WithMessage("min character :3");
        }
    }
}
