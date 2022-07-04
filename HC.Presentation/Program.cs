using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using HC.Application.AutoMapper;
using HC.Application.InversionOfControl;
using HC.Application.Service.Concrete;
using HC.Application.Service.Interface;
using HC.Domain.Entities.Concrete;
using HC.Domain.Repositories.BaseRepository;
using HC.Domain.Repositories.EntityTypeRepositoy;
using HC.Domain.UnitOfWork;
using HC.Infrastructure.Context;
using HC.Infrastructure.Repositories.Concrete;
using HC.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HotCatDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//AddMVC and FluentValidation
builder.Services.AddControllersWithViews().AddFluentValidation();
//AddTransient
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IDepartmentService,DepartmentService>();

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Mapperconfig
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new Mapping());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HotCatDbContext>();

//DependencyResolver

//IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
//.UseServiceProviderFactory(new AutofacServiceProviderFactory())
//.ConfigureContainer<ContainerBuilder>(builder =>
//{
//    builder.RegisterModule(new DependencyResolver());
//});

//CreateHostBuilder(args).Build();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
