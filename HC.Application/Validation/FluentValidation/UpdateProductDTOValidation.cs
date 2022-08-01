﻿using FluentValidation;
using HC.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Validation.FluentValidation
{
    public class UpdateProductDTOValidation : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidation()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Productname cannot be empty").MinimumLength(3).MaximumLength(255).WithMessage("Character min :3 , max : 255");
            RuleFor(x => x.Description).MinimumLength(3).MaximumLength(1000).WithMessage("Character min :3 , max : 1000");
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("UnitPrice cannot be empty");
            RuleFor(x => x.UnitsInStock).NotEmpty().WithMessage("UnitsInStock cannot be empty");
            RuleFor(x => x.SubCategoryId).NotEmpty().WithMessage("SubCategoryId cannot be empty");
            RuleFor(x => x.ImagePath).MinimumLength(3).MaximumLength(500).WithMessage("Character min :3 , max : 500").When(x=>x.ImagePath != String.Empty);
            //RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.CreatedIP).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.CreatedDate).Empty();
            RuleFor(x => x.CreatedComputerName).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.CreatorUserName).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.UpdatedIP).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.UpdatedDate).Empty();
            RuleFor(x => x.UpdatedComputerName).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.UpdaterUserName).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.DeletedIP).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.DeletedComputerName).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.DeleterUserName).MinimumLength(2).When(x=>x.CreatedIP != String.Empty);
            RuleFor(x => x.DeletedDate).Empty();
        }
    }
}
