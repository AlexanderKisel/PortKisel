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
        /// Название компании
        /// </summary>
        public string CompanyZakazchikName { get; set; } = string.Empty;

        /// <summary>
        /// Описание компании
        /// </summary>
        public string CompanyZakazchikDescription { get; set; } = string.Empty;
    }
}
