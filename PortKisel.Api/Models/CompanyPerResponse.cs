namespace PortKisel.Api.Models
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
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Описание компании перевозчика
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
