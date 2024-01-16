using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services;
using CoffeeShop_WebApi.Services.AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess.Repository;
using CoffeeShop.DataAccess.DataAccess.Repository;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using Serilog;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.ServicesLogic.Services.InterfacesServices;
using CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels;

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
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

#region Repositories
builder.Services.AddScoped<ICoffeeShopProductsRepository<Product>, CoffeeShopProductsRepository>();
builder.Services.AddScoped<ICoffeeShopUserRepository<User>, CoffeeShopUserRepository>();
builder.Services.AddScoped<ICoffeeShopUserRepository<UserWithNewsLetter>, NewsLetterRepository>();
builder.Services.AddScoped<ICoffeeShopOrderRepository<Order>,CoffeeShopOrderRepository>();
#endregion

#region Services
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<IServicesProduct<ProductDto>, ServicesProducts>();
builder.Services.AddScoped<IServicesAuth<UserDto>, ServicesAuth>();
builder.Services.AddScoped<IServicesNewsLetter<UserWithNewsLetterDto>, ServicesNewsLetter>();
builder.Services.AddScoped<IServicesOrder<OrderDto>,ServicesOrder>();
#endregion


#region MapperConfig
builder.Services.AddScoped<MapperConfig<User,UserDto>>();
builder.Services.AddScoped<MapperConfig<UserDto, User>>();
builder.Services.AddScoped<MapperConfig<AuthenticateRequest, User>>();
builder.Services.AddScoped<MapperConfig<UserWithNewsLetter, UserWithNewsLetterDto>>();
builder.Services.AddScoped<MapperConfig<UserWithNewsLetterDto, UserWithNewsLetter>>();
builder.Services.AddScoped<MapperConfig<ProductDto, Product>>();
builder.Services.AddScoped<MapperConfig<Product, ProductDto>>();
builder.Services.AddScoped<MapperConfig<OrderDto, Order>>();
builder.Services.AddScoped<MapperConfig<Order, OrderDto>>();
builder.Services.AddScoped < MapperConfig<Category, CategoryDto>>();
#endregion

builder.Services.AddCors(options => options.AddPolicy(name: "corspolicy",
    policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();
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

app.UseSerilogRequestLogging();


app.Run();
