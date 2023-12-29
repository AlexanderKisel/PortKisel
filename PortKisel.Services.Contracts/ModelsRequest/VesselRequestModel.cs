namespace PortKisel.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Модель судна
    /// </summary>
    public class VesselRequestModel
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
        public Guid CompanyPerId { get; set; }

        /// <summary>
        /// Грузоподъемность
        /// </summary>
        public string LoadCapacity { get; set; } = string.Empty;

    }
}
