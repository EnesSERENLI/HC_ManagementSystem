using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Interface
{
    public interface IDepartmentService
    {
        //Add
        Task<string> Create(CreateDepartmentDTO model);
        //Delete
        Task<string> Delete(Guid id);
        //Update
        Task<string> Update(UpdateDepartmentDTO model);
        //List
        Task<List<DepartmentVM>> GetDepartment();
        //Find
        Task<UpdateDepartmentDTO> GetById(Guid id);
    }
}
