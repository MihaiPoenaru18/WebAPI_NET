using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services;
using CoffeeShop_WebApi.Services.AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess.Repository;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop.DataAccess.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

#region ConnectionString
builder.Services.AddDbContext<CoffeeShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
#endregion 

#region Repositories

builder.Services.AddScoped<ICoffeeShopRepository<User>, CoffeeShopUserRepository>();
builder.Services.AddScoped<ICoffeeShopRepository<UserWithNewsLetter>, NewsLetterRepository>();
builder.Services.AddScoped<IServicesAuth<UserDto>, ServicesAuth>();
builder.Services.AddScoped<IServicesNewsLetter<UserWithNewsLetterDto>, ServicesNewsLetter>();
builder.Services.AddScoped<IAuthentication, Authentication>();
#endregion
builder.Services.AddScoped<MapperConfig<User,UserDto>>();
builder.Services.AddScoped<MapperConfig<UserDto, User>>();
builder.Services.AddScoped<MapperConfig<AuthenticateRequest, User>>();
builder.Services.AddScoped<MapperConfig<UserWithNewsLetter, UserWithNewsLetterDto>>();
builder.Services.AddScoped<MapperConfig<UserWithNewsLetterDto, UserWithNewsLetter>>();

builder.Services.AddCors(options => options.AddPolicy(name: "corspolicy",
    policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
{
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    }

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
