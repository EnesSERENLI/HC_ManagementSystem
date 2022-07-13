using HC.Application.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Service.Interface
{
    public interface IRoleService
    {
        //Add
        Task<string> Create(CreateRoleDTO model);
        //Update
        Task<string> Update(UpdateRoleDTO model);
        //Delete
        Task<string> Delete(string id);
        //Find
        Task<UpdateRoleDTO> GetById(string id);
        Task<bool> IsRoleExist(string roleName);
        //List
        IQueryable<IdentityRole> GetRolesList();        
    }
}
