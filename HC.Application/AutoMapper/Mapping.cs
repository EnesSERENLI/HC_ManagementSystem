using AutoMapper;
using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using HC.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            #region Product
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<UpdateProductDTO, ProductVM>().ReverseMap();
            #endregion

            #region Department
            CreateMap<Department, CreateDepartmentDTO>().ReverseMap();
            //.ForMember(x => x.CreatedIP, option => option.Ignore())
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Department, UpdateDepartmentDTO>().ReverseMap();
            CreateMap<UpdateDepartmentDTO, DepartmentVM>().ReverseMap();
            #endregion

            #region Category
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<UpdateCategoryDTO, CategoryVM>().ReverseMap();
            #endregion

            #region SubCategory
            CreateMap<SubCategory, CreateSubCategoryDTO>().ReverseMap();
            CreateMap<SubCategory, SubCategoryVM>().ReverseMap();
            CreateMap<SubCategory, UpdateSubCategoryDTO>().ReverseMap();
            CreateMap<UpdateSubCategoryDTO, SubCategoryVM>().ReverseMap();
            #endregion

            #region Employee
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDTO>().ReverseMap();
            CreateMap<UpdateEmployeeDTO, EmployeeVM>().ReverseMap();
            #endregion

            #region AppUser
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, AppUserVM>().ReverseMap();
            CreateMap<UpdateUserDTO, AppUserVM>().ReverseMap();
            #endregion

            #region Role
            CreateMap<IdentityRole, CreateRoleDTO>().ReverseMap();
            CreateMap<IdentityRole, UpdateRoleDTO>().ReverseMap();
            CreateMap<UpdateRoleDTO, IdentityRole>().ReverseMap(); 
            #endregion

        }
    }
}
