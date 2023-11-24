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
        /// Название компании перевозчика
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание компании
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
