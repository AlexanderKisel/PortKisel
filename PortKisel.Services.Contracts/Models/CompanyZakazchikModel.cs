namespace PortKisel.Services.Contracts.Models
{
    /// <summary>
    /// Модель компании заказчика
    /// </summary>
    public class CompanyZakazchikModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
