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
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();

            builder.RegisterType<CreateDepartmentDTOValidation>().As<IValidator<CreateDepartmentDTO>>().InstancePerLifetimeScope();

            builder.RegisterType<UpdateDepartmentDTOValidation>().As<IValidator<UpdateDepartmentDTO>>().InstancePerLifetimeScope();


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
