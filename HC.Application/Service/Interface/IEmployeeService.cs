using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Interface
{
    public interface IEmployeeService
    {
        //Add
        Task<string> Create(CreateEmployeeDTO model);
        //Delete
        Task<string> Delete(Guid id);
        //Update
        Task<string> Update(UpdateEmployeeDTO model);
        //List
        Task<List<EmployeeVM>> GetEmployees();

        Task<List<EmployeeVM>> GetDefaultEmployees();
        //Find
        Task<UpdateEmployeeDTO> GetById(Guid id);
    }
}
