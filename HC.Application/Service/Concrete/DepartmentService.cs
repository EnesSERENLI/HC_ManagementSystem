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

            await _unitOfWork.DepartmentRepository.Add(department);

            return "Department Added!";
        }

        public async Task<string> Delete(Guid id)
        {
            var deleted = await _unitOfWork.DepartmentRepository.GetById(id);

            deleted.Status = Domain.Enums.Status.Deleted;

            await _unitOfWork.DepartmentRepository.Update(deleted);
            return "Department deleted!.";
        }

        public async Task<UpdateDepartmentDTO> GetById(Guid id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetFilteredFirstOrDefault(selector: x => new DepartmentVM
            {
                ID = x.ID,
                DepartmentName = x.DepartmentName,
            },
            expression: x => x.ID == id);

            var model = _mapper.Map<UpdateDepartmentDTO>(department);

            return model;            
        }

        public async Task<List<DepartmentVM>> GetDepartment()
        {
            var departmentList =await _unitOfWork.DepartmentRepository.GetFilteredFirstOrDefaults(selector: x => new DepartmentVM
            {
                ID = x.ID,
                DepartmentName = x.DepartmentName
            },
            expression: x => x.Status == Domain.Enums.Status.Active,
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
