using PortKisel.Context;
using PortKisel.Context.Contracts;
using PortKisel.Repositories;
using PortKisel.Repositories.Contracts;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<ICargoReadRepository, CargoReadRepository>();

builder.Services.AddScoped<ICompanyPerService, CompanyPerService>();
builder.Services.AddScoped<ICompanyPerReadRepository, CompanyPerReadRepository>();

builder.Services.AddScoped<ICompanyZakazchikService, CompanyZakazchikService>();
builder.Services.AddScoped<ICompanyZakazchikReadRepository, CompanyZakazchikReadRepository>();

builder.Services.AddScoped<IDocumentiService, DocumentiService>();
builder.Services.AddScoped<IDocumentiReadRepository, DocumentiReadRepository>();

builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IStaffReadRepository, StaffReadRepository>();

builder.Services.AddScoped<IVesselService, VesselService>();
builder.Services.AddScoped<IVesselReadRepository, VesselReadRepository>();

builder.Services.AddSingleton<IPortContext, PortContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
