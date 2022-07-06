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
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Create(CreateDepartmentDTO model)
        {
            var department = _mapper.Map<Department>(model);

            var result = await _unitOfWork.DepartmentRepository.Any(x=>x.DepartmentName == model.DepartmentName);

            if (result)
                return "This department already exists!";

            await _unitOfWork.DepartmentRepository.Add(department);

            return "Department Added!";
        }

        public async Task<string> Delete(Guid id)
        {
            await _unitOfWork.DepartmentRepository.Delete(id);

            return "Department deleted!.";
        }

        public async Task<UpdateDepartmentDTO> GetById(Guid id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetFilteredFirstOrDefault(selector: x => new Department
            {
                ID = x.ID,
                DepartmentName = x.DepartmentName,
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

            var model = _mapper.Map<UpdateDepartmentDTO>(department);

            return model;            
        }

        public async Task<List<DepartmentVM>> GetDefaultDepartments()
        {
            var departmentList = await _unitOfWork.DepartmentRepository.GetFilteredFirstOrDefaults(selector: x => new DepartmentVM
            {
                ID = x.ID,
                DepartmentName = x.DepartmentName
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated,
            orderBy: x => x.OrderBy(x => x.DepartmentName)
            );

            return departmentList;
        }

        public async Task<List<DepartmentVM>> GetDepartments()
        {
            var departmentList =await _unitOfWork.DepartmentRepository.GetFilteredFirstOrDefaults(selector: x => new DepartmentVM
            {
                ID = x.ID,
                DepartmentName = x.DepartmentName,
                Status = x.Status
            },
            expression: x => x.Status == Domain.Enums.Status.Active || x.Status == Domain.Enums.Status.Updated || x.Status == Domain.Enums.Status.Deleted,
            orderBy: x => x.OrderBy(x => x.DepartmentName)
            );

            return departmentList;
        }

        public async Task<string> Update(UpdateDepartmentDTO model)
        {
            var department = _mapper.Map<Department>(model);

            await _unitOfWork.DepartmentRepository.Update(department);

            return "Department updated!";
        }
    }
}
