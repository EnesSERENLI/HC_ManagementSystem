using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using HC.Application.AutoMapper;
using HC.Application.InversionOfControl;
using HC.Application.Service.Concrete;
using HC.Application.Service.Interface;
using HC.Domain.Entities.Concrete;
using HC.Domain.UnitOfWork;
using HC.Infrastructure.Context;
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
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISubCategoryService, SubCategoryService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IAppUserService, AppUserService>();
builder.Services.AddTransient<IRoleService, RoleService>();

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Mapperconfig
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new Mapping());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddIdentity<AppUser, AppUserRole>().AddEntityFrameworkStores<HotCatDbContext>();
//builder.Services.AddDefaultIdentity<AppUser>().AddEntityFrameworkStores<HotCatDbContext>();

//Identity
builder.Services.Configure<IdentityOptions>(x =>
{
    //sifre yapilandirmasi
    x.Password.RequiredLength = 6;
    x.Password.RequireDigit = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
    //giris yapilandirmasi
    x.SignIn.RequireConfirmedPhoneNumber = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.SignIn.RequireConfirmedEmail = true;
    x.User.RequireUniqueEmail = false;
});

//Cookies
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = new PathString("/Account/SignIn");
    x.AccessDeniedPath = new PathString("/Account/SignIn");

    x.Cookie = new CookieBuilder()
    {
        Name = "HotCatCerez"
    };
    x.SlidingExpiration = true;
    x.ExpireTimeSpan = TimeSpan.FromDays(1);
});

//Session
builder.Services.AddSession(x =>
{
    
});

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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(x => {

    x.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{Controller=home}/{Action=index}/{id?}" //Area Route
    );

    x.MapControllerRoute(
        name: "default",
        pattern: "{Controller=Home}/{Action=Index}/{id?}" //Default Route
    );
});

//app.MapRazorPages();

app.Run();
