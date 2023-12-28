namespace PortKisel.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Модель компании перевозчика
    /// </summary>
    public class CompanyPerRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название компании перевозчика
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание компании
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
