using FluentValidation;
using HC.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Validation.FluentValidation
{
    public class CreateProductDTOValidation : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidation()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Productname cannot be empty").MinimumLength(3).MaximumLength(255).WithMessage("Character min :3 , max : 255");
            RuleFor(x => x.Description).MinimumLength(3).MaximumLength(1000).WithMessage("Character min :3 , max : 1000");
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("UnitPrice cannot be empty").GreaterThan(0).WithMessage("UnitPrice must be greater than 0");
            RuleFor(x => x.UnitsInStock).NotEmpty().WithMessage("UnitsInStock cannot be empty").GreaterThan(Convert.ToInt16(0)).WithMessage("UnitsInStock must be greater than 0");
            RuleFor(x => x.SubCategoryId).NotEmpty().WithMessage("SubCategoryId cannot be empty");
            RuleFor(x => x.ImagePath).MinimumLength(3).MaximumLength(500).WithMessage("Character min :3 , max : 500").When(x=>x.ImagePath != String.Empty);
            RuleFor(x => x.Image).NotEmpty();
        }
    }
}
