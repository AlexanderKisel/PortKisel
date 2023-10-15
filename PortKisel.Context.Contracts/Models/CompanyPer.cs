namespace PortKisel.Context.Contracts.Models
{
    public class CompanyPer : BaseAuditEntity
    {
        /// <summary>
        /// Название компании перевозчика
        /// </summary>
        public string CompanyPerName { get; set; } = string.Empty;

        /// <summary>
        /// Описание компании
        /// </summary>
        public string CompanyPerDescription { get; set;} = string.Empty;
    }
}
