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
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Create(CreateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);

            var result = await _unitOfWork.CategoryRepository.Any(x => x.CategoryName == model.CategoryName);

            if (result)
            {
                return "This category already exists!";
            }
            await _unitOfWork.CategoryRepository.Add(category);

            return "Category added!";
        }

        public async Task<string> Delete(Guid id)
        {
            await _unitOfWork.CategoryRepository.GetById(id);

            return "Category deleted!";
        }

        public async Task<UpdateCategoryDTO> GetById(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefault(selector: x => new CategoryVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description
            },
            expression: x => x.ID == id);

            var model = _mapper.Map<UpdateCategoryDTO>(category); //category'i al updateCategoryDTO'ya çevir. model'in içine at

            return model; //model'i dön.
        }

        public async Task<List<CategoryVM>> GetCategories()
        {
            var categoryList = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefaults(selector: x => new CategoryVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated || x.Status == Domain.Enums.Status.Deleted
            );

            return categoryList;
        }

        public async Task<List<CategoryVM>> GetDefaultCategories()
        {
            var categoryList = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefaults(selector: x => new CategoryVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated
            );

            return categoryList;
        }

        public Task<string> Update(UpdateCategoryDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
