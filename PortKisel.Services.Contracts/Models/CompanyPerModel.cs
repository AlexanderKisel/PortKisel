namespace PortKisel.Services.Contracts.Models
{
    /// <summary>
    /// Модель компании перевозчика
    /// </summary>
    public class CompanyPerModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        public string CompanyPerName { get; set; } = string.Empty;

        /// <summary>
        /// Описание компании
        /// </summary>
        public string CompanyPerDescription { get; set; } = string.Empty;
    }
}
