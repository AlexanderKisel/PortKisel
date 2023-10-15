namespace PortKisel.Models
{
    /// <summary>
    /// Модель ответа сущности компания заказчик
    /// </summary>
    public class CompanyZakazchikResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование компании заказчика
        /// </summary>
        public string CompanyZakazchikName { get; set; } = string.Empty;
        /// <summary>
        /// Описание компании заказчика
        /// </summary>
        public string CompanyZakazchikDescription { get; set; } = string.Empty;
    }
}
