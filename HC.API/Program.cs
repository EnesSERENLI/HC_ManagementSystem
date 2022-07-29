using AutoMapper;
using FluentValidation.AspNetCore;
using HC.API.MiddleWares;
using HC.API.Utils.Concrete;
using HC.API.Utils.Interface;
using HC.Application.AutoMapper;
using HC.Application.Service.Concrete;
using HC.Application.Service.Interface;
using HC.Domain.UnitOfWork;
using HC.Infrastructure.Context;
using HC.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers().AddFluentValidation();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HotCatDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddSingleton<ILoggerService, FileLogger>(); //Eðer console'a yazdýrmak istersem tek yapmam gereken FileLogger yerine ConsoleLogger yazmak..
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Mapperconfig
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new Mapping());
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("HCAPI", new OpenApiInfo()
    {
        Title = "RESTful API",
        Version = "V1",
        Description = "Restful Api Trials",
        Contact = new OpenApiContact()
        {
            Email = "enes.serenli@hotmail.com",
            Name = "Enes Serenli",
            Url = new Uri("https://github.com/EnesSERENLI")
        },
        License = new OpenApiLicense()
        {
            Name = "MIT Licence",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/HCAPI/swagger.json", "HC API");
});

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCustomExceptionMiddle();

app.UseEndpoints(endPoints =>
{
    endPoints.MapControllers();
});

app.Run();
