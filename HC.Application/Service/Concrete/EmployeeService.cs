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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Create(CreateEmployeeDTO model)
        {
            var employee = _mapper.Map<Employee>(model);

            await _unitOfWork.EmployeeRepository.Add(employee); //burada sorgulama yapmaya gerek yok. Aynı isimde 2 veya daha fazla çalışan olabilir.

            return "Employee added!";
        }

        public async Task<string> Delete(Guid id)
        {
            await _unitOfWork.EmployeeRepository.Delete(id);

            return "Employee deleted!";
        }

        public async Task<UpdateEmployeeDTO> GetById(Guid id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetFilteredFirstOrDefault(selector: x => new EmployeeVM
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title,
                Address = x.Address,
                Department = x.Department.DepartmentName,
                DepartmentId = x.DepertmentId               
            },
            expression: x=> x.ID == id);

            var model = _mapper.Map<UpdateEmployeeDTO>(employee);

            return model;
        }

        public async Task<List<EmployeeVM>> GetDefaultEmployees()
        {
            var employeeList = await _unitOfWork.EmployeeRepository.GetFilteredFirstOrDefaults(selector: x=> new EmployeeVM
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title,
                Address = x.Address,
                Department = x.Department.DepartmentName
            },
            expression: x=> x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated);

            return employeeList;
        }

        public async Task<List<EmployeeVM>> GetEmployees()
        {
            var employeeList = await _unitOfWork.EmployeeRepository.GetFilteredFirstOrDefaults(selector: x => new EmployeeVM
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title,
                Address = x.Address,
                Department = x.Department.DepartmentName
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated || x.Status == Domain.Enums.Status.Deleted);

            return employeeList;
        }

        public async Task<string> Update(UpdateEmployeeDTO model)
        {
            try
            {
                var updated = _mapper.Map<Employee>(model);

                await _unitOfWork.EmployeeRepository.Update(updated);

                return "Employee updated!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
