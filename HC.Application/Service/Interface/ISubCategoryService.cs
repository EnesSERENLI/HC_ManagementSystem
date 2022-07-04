using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Interface
{
    public interface ISubCategoryService
    {
        //Add
        Task<string> Create(CreateSubCategoryDTO model);
        //Delete
        Task<string> Delete(Guid id);
        //Update
        Task<string> Update(UpdateSubCategoryDTO model);
        //List
        Task<List<SubCategoryVM>> GetSubCategories();

        Task<List<SubCategoryVM>> GetDefaultSubCategories();
        //Find
        Task<UpdateSubCategoryDTO> GetById(Guid id);
    }
}
