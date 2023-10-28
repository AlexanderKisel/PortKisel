namespace PortKisel.Services.Contracts.Models
{
    /// <summary>
    /// Модель груза
    /// </summary>
    public class CargoModel
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
        public CompanyZakazchikModel? CompanyZakazchikName { get; set; }
    }
}
