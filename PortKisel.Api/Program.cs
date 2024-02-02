using Microsoft.EntityFrameworkCore;
using PortKisel.Api.Infrastructures;
using PortKisel.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x =>
{
    x.Filters.Add<PortExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.GetSwaggerDocument();
builder.Services.AddDependencies();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<PortContext>(options => options.UseSqlServer(conString),
    ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerDocumentUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }