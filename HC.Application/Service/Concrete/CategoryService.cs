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
            try
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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Delete(Guid id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.Delete(id);

                return "Category deleted!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<UpdateCategoryDTO> GetById(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefault(selector: x => new Category
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
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
                Description = x.Description,
                Status = x.Status
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
                Status = x.Status,
                Description = x.Description
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated
            );

            return categoryList;
        }

        public async Task<string> Update(UpdateCategoryDTO model)
        {
            try
            {
                var updated = _mapper.Map<Category>(model);

                await _unitOfWork.CategoryRepository.Update(updated);

                return "Category updated!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
