using FluentValidation;
using HC.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Validation.FluentValidation
{
    public class LoginDTOValidation : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a Username!").MinimumLength(3).WithMessage("Username must be at least 3 characters!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter a Password!").MinimumLength(6).WithMessage("Password must be at least 6 characters!");
        }
    }
}
