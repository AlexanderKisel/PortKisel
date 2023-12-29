namespace PortKisel.Context.Contracts.Models
{
    public class CompanyPer : BaseAuditEntity
    {
        /// <summary>
        /// Название компании перевозчика
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание компании
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Список судов
        /// </summary>
        public IEnumerable<Vessel>? Vessels { get; set; }
    }
}
