namespace PortKisel.Api.ModelsRequest.CompanyZakazchik
{
    public class CreateCompanyZakazhikRequest
    {
        /// <summary>
        /// Название компании заказчика
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
