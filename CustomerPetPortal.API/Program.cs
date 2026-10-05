using CustomerPetPortal.API.Data;
using CustomerPetPortal.API.Repositories;
using CustomerPetPortal.API.Services;
using CustomerPetPortal.API.Repositories.Interfaces;
using CustomerPetPortal.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CustomerPetPortal.API.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));
// Dependency Injection
builder.Services.AddScoped<ICustomerRepository,
    CustomerRepository>();

builder.Services.AddScoped<ICustomerService,
    CustomerService>();
builder.Services.AddScoped<
    IPetRepository,
    PetRepository>();

builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<
    IDashboardService,
    DashboardService>();
builder.Services.AddScoped<
    IPolicyRepository,
    PolicyRepository>();

builder.Services.AddScoped<
    IPolicyService,
    PolicyService>();
builder.Services.AddScoped<
    IClaimRepository,
    ClaimRepository>();

builder.Services.AddScoped<
    IClaimService,
    ClaimService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<
    IAddressRepository,
    AddressRepository>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Angular",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
c.DefaultModelsExpandDepth(-1);
});

app.UseCors("Angular");

app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();
