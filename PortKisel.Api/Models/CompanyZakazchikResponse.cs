namespace PortKisel.Api.Models
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
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Описание компании заказчика
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
