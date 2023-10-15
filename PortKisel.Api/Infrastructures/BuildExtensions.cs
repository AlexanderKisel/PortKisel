using PortKisel.Context;
using PortKisel.Context.Contracts;
using PortKisel.Repositories;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Implementations;
using PortKisel.Services.AutoMappers;

namespace PortKisel.Api.Infrastructures
{
    public class BuildExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICargoService, CargoService>();
            services.AddScoped<ICargoReadRepository, CargoReadRepository>();
            services.AddScoped<IPortContext, PortContext>();

            services.AddScoped<ICompanyPerService, CompanyPerService>();
            services.AddScoped<ICompanyPerReadRepository, CompanyPerReadRepository>();
            services.AddScoped<IPortContext, PortContext>();

            services.AddScoped<ICompanyZakazchikService, CompanyZakazchikService>();
            services.AddScoped<ICompanyZakazchikReadRepository, CompanyZakazchikReadRepository>();
            services.AddScoped<IPortContext, PortContext>();

            services.AddScoped<IDocumentiService, DocumentiService>();
            services.AddScoped<IDocumentiReadRepository, DocumentiReadRepository>();
            services.AddScoped<IPortContext, PortContext>();

            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IStaffReadRepository, StaffReadRepository>();
            services.AddScoped<IPortContext, PortContext>();

            services.AddScoped<IVesselService, VesselService>();
            services.AddScoped<IVesselReadRepository, VesselReadRepository>();
            services.AddScoped<IPortContext, PortContext>();

            services.AddScoped<IPortContext, PortContext>();

            services.AddAutoMapper(typeof(ServiceProfile));
        }
    }
}
