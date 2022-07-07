using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Interface
{
    public interface IProductService
    {
        //Add
        Task<string> Create(CreateProductDTO model);
        //Delete
        Task<string> Delete(Guid id);
        //Update
        Task<string> Update(UpdateProductDTO model);
        //List
        Task<List<ProductVM>> GetProducts();
        Task<List<ProductVM>> GetDefaultProducts();
        Task<List<ProductVM>> GetProductsByCategory(Guid subCategoryId);
        //Find
        Task<UpdateProductDTO> GetById(Guid id);
    }
}
