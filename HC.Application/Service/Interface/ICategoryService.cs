using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Interface
{
    public interface ICategoryService
    {
        //Add
        Task<string> Create(CreateCategoryDTO model);
        //Delete
        Task<string> Delete(Guid id);
        //Update
        Task<string> Update(UpdateCategoryDTO model);
        //List
        Task<List<CategoryVM>> GetCategories();

        Task<List<CategoryVM>> GetDefaultCategories();
        //Find
        Task<UpdateCategoryDTO> GetById(Guid id);
    }
}
