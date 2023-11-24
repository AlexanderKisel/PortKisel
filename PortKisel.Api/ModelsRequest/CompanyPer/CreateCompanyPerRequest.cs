namespace PortKisel.Api.ModelsRequest.CompanyPer
{
    public class CreateCompanyPerRequest
    {
        /// <summary>
        /// Название компании перевозчика
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание компании
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
