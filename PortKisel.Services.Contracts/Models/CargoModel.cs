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
        /// Описание грузы
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Масса груза
        /// </summary>
        public string Weight { get; set; } = string.Empty;

        /// <summary>
        /// <see cref="CompanyZakazchik"/>
        /// </summary>
        public CompanyZakazchikModel? CompanyZakazchik { get; set; }
    }
}
