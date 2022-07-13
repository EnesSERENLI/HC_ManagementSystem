using AutoMapper;
using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Domain.Entities.Concrete;
using HC.Domain.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppUserRole> _roleManager;

        public RoleService(IUnitOfWork unitOfWork,IMapper mapper,RoleManager<AppUserRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<string> Create(CreateRoleDTO model)
        {
            var roleExist =await IsRoleExist(model.Name);
            if (roleExist)
                return "This role already exist!";

            var role = _mapper.Map<AppUserRole>(model);
            IdentityResult result = await _roleManager.CreateAsync(role);
            await _unitOfWork.Approve();
            if (result.Succeeded)
                return "Role has been created!";
            else
                return "Role hasn't been created! Try again after a while!";
        }

        public async Task<string> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return "No such role found!";

           IdentityResult result =  await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return "Role deleted!";
            return "Role hasn't been deleted! Try again after a while";
        }

        public async Task<UpdateRoleDTO> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var updateRoleDTO = _mapper.Map<UpdateRoleDTO>(role);

            return updateRoleDTO;
        }

        public IQueryable<AppUserRole> GetRolesList()
        {
            var roleList = _roleManager.Roles;

            return roleList;
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            return roleExist;
        }

        public async Task<string> Update(UpdateRoleDTO model)
        {
            var role = _mapper.Map<AppUserRole>(model);

            IdentityResult result = await _roleManager.UpdateAsync(role);
            await _unitOfWork.Approve();
            if (result.Succeeded)
                return "Role updated!";
            return "Role hasn't been updated!";
        }
    }
}
