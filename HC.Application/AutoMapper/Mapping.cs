using AutoMapper;
using HC.Application.Models.DTO;
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
            CreateMap<Department, CreateDepartmentDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<SubCategory, CreateSubCategoryDTO>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
        }
    }
}
