namespace PortKisel.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Модель компании заказчика
    /// </summary>
    public class CompanyZakazchikRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название компании заказчика
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
