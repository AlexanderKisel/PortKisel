namespace PortKisel.Context.Contracts.Models
{
    /// <summary>
    /// Груз
    /// </summary>
    public class Cargo : BaseAuditEntity
    {
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
        public Guid CompanyZakazchikId { get; set; }

        /// <summary>
        /// Компания заказчик
        /// </summary>
        public CompanyZakazchik? CompanyZakazchik { get; set; }

        /// <summary>
        /// Список документов
        /// </summary>
        public IEnumerable<Documenti>? Documenti { get; set; }

    }
}
