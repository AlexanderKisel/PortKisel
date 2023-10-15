namespace PortKisel.Models
{
    /// <summary>
    /// Модель ответа сущности компания перевозчик
    /// </summary>
    public class CompanyPerResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование компании перевозчика
        /// </summary>
        public string CompanyPerName { get; set; } = string.Empty;
        /// <summary>
        /// Описание компании перевозчика
        /// </summary>
        public string CompanyPerDescription { get; set; } = string.Empty;
    }
}
