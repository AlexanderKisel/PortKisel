namespace PortKisel.Context.Contracts.Models
{
    public class CompanyZakazchik: BaseAuditEntity
    {
        /// <summary>
        /// Название компании заказчика
        /// </summary>
        public string CompanyZakazchikName { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string CompanyZakazchikDescription { get; set; } = string.Empty;
    }
}
