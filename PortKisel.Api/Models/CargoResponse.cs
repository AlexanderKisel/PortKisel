namespace PortKisel.Models
{
    /// <summary>
    /// Модель ответа сущности груза
    /// </summary>
    public class CargoResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название груза
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание груза
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Вес груза
        /// </summary>
        public string Weight { get; set; } = string.Empty;

        /// <summary>
        /// Компания заказчик (владедец груза)
        /// </summary>
        public Guid CompanyZakazchikId { get; set; }
    }
}
