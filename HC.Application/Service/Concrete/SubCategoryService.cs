using AutoMapper;
using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using HC.Application.Service.Interface;
using HC.Domain.Entities.Concrete;
using HC.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Concrete
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubCategoryService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Create(CreateSubCategoryDTO model)
        {
            var subCategory = _mapper.Map<SubCategory>(model);

            var result = await _unitOfWork.SubCategoryRepository.Any(x=>x.SubCategoryName == subCategory.SubCategoryName);

            if (result)
            {
                return "This SubCategory already exists!";
            }
            await _unitOfWork.SubCategoryRepository.Add(subCategory);

            return "SubCategory added!";
        }

        public async Task<string> Delete(Guid id)
        {
            await _unitOfWork.SubCategoryRepository.Delete(id);

            return "SubCategory deleted!";
        }

        public async Task<UpdateSubCategoryDTO> GetById(Guid id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.GetFilteredFirstOrDefault(selector: x => new SubCategory
            {
                ID = x.ID,
                SubCategoryName = x.SubCategoryName,
                Description = x.Description,
                CreatedIP = x.CreatedIP,
                CreatedDate = x.CreatedDate,
                CreatedComputerName = x.CreatedComputerName,
                UpdatedComputerName = x.UpdatedComputerName,
                UpdatedIP = x.UpdatedIP,
                UpdatedDate = x.UpdatedDate,
                DeletedIP = x.DeletedIP,
                DeletedDate = x.DeletedDate,
                DeletedComputerName = x.DeletedComputerName
            },
            expression: x=> x.ID == id);

            var model = _mapper.Map<UpdateSubCategoryDTO>(subCategory);

            return model;
        }

        public async Task<List<SubCategoryVM>> GetSubCategories()
        {
            var subCategories = await _unitOfWork.SubCategoryRepository.GetFilteredFirstOrDefaults(selector: x => new SubCategoryVM
            {
                ID = x.ID,
                SubCategoryName = x.SubCategoryName,
                Status = x.Status,
                Description = x.Description
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated || x.Status == Domain.Enums.Status.Deleted
            );
            return subCategories;
        }

        public async Task<List<SubCategoryVM>> GetDefaultSubCategories()
        {
            var subCategories = await _unitOfWork.SubCategoryRepository.GetFilteredFirstOrDefaults(selector: x => new SubCategoryVM
            {
                ID = x.ID,
                SubCategoryName = x.SubCategoryName,
                CategoryName = x.Category.CategoryName,
                Description = x.Description,
                Status = x.Status               
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated
            );
            return subCategories;
        }

        public async Task<string> Update(UpdateSubCategoryDTO model)
        {
            var updated = _mapper.Map<SubCategory>(model);

            await _unitOfWork.SubCategoryRepository.Update(updated);

            return "SubCategory updated!";
        }
    }
}
