namespace PortKisel.Api.Models
{
    /// <summary>
    /// Модель ответа сущности документов
    /// </summary>
    public class DocumentiResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string NumberDoc { get; set; } = string.Empty;

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssaedAt { get; set; }

        /// <summary>
        /// <see cref="CargoRequestModel"/>
        /// </summary>
        public string CargoName { get; set; }

        /// <summary>
        /// <see cref="CargoRequestModel"/>
        /// </summary>
        public string CargoWeight { get; set; }

        /// <summary>
        /// <see cref="VesselRequestModel"/>
        /// </summary>
        public string VesselName { get; set; }

        /// <summary>
        /// <see cref="VesselRequestModel"/>
        /// </summary>
        public string VesselLoadCapacity { get; set; }

        /// <summary>
        /// Пост
        /// </summary>
        public string StaffName { get; set; }
    }
}
