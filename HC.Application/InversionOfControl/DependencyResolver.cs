using Autofac;
using AutoMapper;
using FluentValidation;
using HC.Application.AutoMapper;
using HC.Application.Models.DTO;
using HC.Application.Service.Concrete;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using HC.Domain.UnitOfWork;
using HC.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.InversionOfControl
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder) //bu çalışmıyor bakıcam.
        {
            #region UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            #endregion

            #region Services
            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderDetailService>().As<IOrderDetailService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<SubCategoryService>().As<ISubCategoryService>().InstancePerLifetimeScope();
            #endregion

            #region FluentValidation
            builder.RegisterType<CreateDepartmentDTOValidation>().As<IValidator<CreateDepartmentDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateDepartmentDTOValidation>().As<IValidator<UpdateDepartmentDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateCategoryDTOValidation>().As<IValidator<CreateCategoryDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateCategoryDTOValidation>().As<IValidator<UpdateCategoryDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateEmployeeDTOValidation>().As<IValidator<CreateEmployeeDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateEmployeeDTOValidation>().As<IValidator<UpdateEmployeeDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateProductDTOValidation>().As<IValidator<CreateProductDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateProductDTOValidation>().As<IValidator<UpdateProductDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateRoleDTOValidation>().As<IValidator<CreateRoleDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateRoleDTOValidation>().As<IValidator<UpdateRoleDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<CreateSubCategoryDTOValidation>().As<IValidator<CreateSubCategoryDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateSubCategoryDTOValidation>().As<IValidator<UpdateSubCategoryDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterDTOValidation>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<LoginDTOValidation>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateUserDTOValidation>().As<IValidator<UpdateUserDTO>>().InstancePerLifetimeScope(); 
            #endregion


            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Mapping>();
                }
                )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope(); 
            #endregion

            base.Load(builder);
        }
    }
}
