using Microsoft.OpenApi.Models;

namespace PortKisel.Api.Infrastructures
{
    static internal class DocumentExtensions
    {
        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Cargo", new OpenApiInfo { Title = "Сущность грузы", Version = "v1" });
                c.SwaggerDoc("CompanyPer", new OpenApiInfo { Title = "Сущность компании перевозчики", Version = "v1" });
                c.SwaggerDoc("CompanyZakazchik", new OpenApiInfo { Title = "Сущность компании заказчики", Version = "v1" });
                c.SwaggerDoc("Documenti", new OpenApiInfo { Title = "Сущность документы", Version = "v1" });
                c.SwaggerDoc("Staff", new OpenApiInfo { Title = "Сущность сотрудники", Version = "v1" });
                c.SwaggerDoc("Vessel", new OpenApiInfo { Title = "Сущность судна", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "PortKisel.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Cargo/swagger.json", "Грузы");
                x.SwaggerEndpoint("CompanyPer/swagger.json", "Компании перевозчики");
                x.SwaggerEndpoint("CompanyZakazchik/swagger.json", "Компании заказчики");
                x.SwaggerEndpoint("Documenti/swagger.json", "Документы");
                x.SwaggerEndpoint("Staff/swagger.json", "Сотрудники");
                x.SwaggerEndpoint("Vessel/swagger.json", "Судна");
            });
        }
    }
}
