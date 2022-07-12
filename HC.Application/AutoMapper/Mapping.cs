using AutoMapper;
using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using HC.Domain.Entities.Concrete;
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
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<UpdateProductDTO, ProductVM>().ReverseMap();

            CreateMap<Department, CreateDepartmentDTO>().ReverseMap();
                //.ForMember(x => x.CreatedIP, option => option.Ignore())
            CreateMap<Department, DepartmentVM>().ReverseMap();
            CreateMap<Department, UpdateDepartmentDTO>().ReverseMap();
            CreateMap<UpdateDepartmentDTO, DepartmentVM>().ReverseMap();

            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<UpdateCategoryDTO, CategoryVM>().ReverseMap();

            CreateMap<SubCategory, CreateSubCategoryDTO>().ReverseMap();
            CreateMap<SubCategory, SubCategoryVM>().ReverseMap();
            CreateMap<SubCategory, UpdateSubCategoryDTO>().ReverseMap();
            CreateMap<UpdateSubCategoryDTO, SubCategoryVM>().ReverseMap();

            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeVM>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDTO>().ReverseMap();
            CreateMap<UpdateEmployeeDTO, EmployeeVM>().ReverseMap();

            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, AppUserVM>().ReverseMap();
            CreateMap<UpdateUserDTO, AppUserVM>().ReverseMap();
        }
    }
}
