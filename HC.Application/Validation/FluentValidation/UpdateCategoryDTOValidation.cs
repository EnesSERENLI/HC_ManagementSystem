using FluentValidation;
using HC.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Validation.FluentValidation
{
    public class UpdateCategoryDTOValidation : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDTOValidation()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("CategoryName cannot be empty!").MinimumLength(3).MaximumLength(255).WithMessage("Character min :3 , max : 255");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty!").MinimumLength(3).MaximumLength(1000).WithMessage("Character min :3 , max : 1000");
        }
    }
}
