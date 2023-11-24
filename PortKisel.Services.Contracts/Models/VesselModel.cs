namespace PortKisel.Services.Contracts.Models
{
    /// <summary>
    /// Модель судна
    /// </summary>
    public class VesselModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название судна
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Компания перевозчик
        /// </summary>
        public CompanyPerModel CompanyPer { get; set; }

        /// <summary>
        /// Грузоподъемность
        /// </summary>
        public string LoadCapacity { get; set; } = string.Empty;

    }
}
