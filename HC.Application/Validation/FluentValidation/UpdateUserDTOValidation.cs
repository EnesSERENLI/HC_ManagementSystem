using FluentValidation;
using HC.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Validation.FluentValidation
{
    public class UpdateUserDTOValidation : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidation()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName cannot be empty!").MinimumLength(3).WithMessage("FullName must be at least 3 characters!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty!").MinimumLength(3).WithMessage("UserName must be at least 3 characters!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty!").EmailAddress().WithMessage("Email address must be in Email format!");
            RuleFor(x => x.Address).MinimumLength(3).MaximumLength(500).WithMessage("Character min: 3 , max : 500");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty!").MinimumLength(6).MinimumLength(20).WithMessage("Password must be between a minimum of 6 and a maximum of 20 characters.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword cannot be empty!").Equal(x => x.Password).WithMessage("Passwords do not match!");
            RuleFor(x => x.ImagePath).MinimumLength(3).MaximumLength(500).WithMessage("Character min :3 , max : 500");
        }
    }
}
