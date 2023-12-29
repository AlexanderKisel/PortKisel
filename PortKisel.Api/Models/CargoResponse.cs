namespace PortKisel.Api.Models
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
        public string Name { get; set; }

        /// <summary>
        /// Описание груза
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Вес груза
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Компания заказчик (владедец груза)
        /// </summary>
        public string? CompanyZakazchikName { get; set; }
    }
}
