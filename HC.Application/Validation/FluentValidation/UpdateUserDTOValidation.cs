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
            RuleFor(x => x.FullName).MinimumLength(3).WithMessage("FullName must be at least 3 characters!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty!").MinimumLength(3).WithMessage("UserName must be at least 3 characters!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email address must be in Email format!");
            RuleFor(x => x.Address).MaximumLength(500).WithMessage("Character max : 500");
            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(20).WithMessage("Password must be between a minimum of 6 and a maximum of 20 characters.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match!");
            RuleFor(x => x.ImagePath).MaximumLength(500).WithMessage("Character max : 500");
        }
    }
}
