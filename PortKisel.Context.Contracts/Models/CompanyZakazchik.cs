namespace PortKisel.Context.Contracts.Models
{
    public class CompanyZakazchik : BaseAuditEntity
    {
        /// <summary>
        /// Название компании заказчика
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Список грузов
        /// </summary>
        public IEnumerable<Cargo>? Cargo { get; set; }
    }
}
