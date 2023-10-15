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
