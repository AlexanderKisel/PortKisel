using PortKisel.Services.Contracts.Models;

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
        /// <see cref="CargoModel"/>
        /// </summary>
        public string CargoName { get; set; }

        /// <summary>
        /// <see cref="CargoModel"/>
        /// </summary>
        public string CargoWeight { get; set; }

        /// <summary>
        /// <see cref="VesselModel"/>
        /// </summary>
        public string VesselName { get; set; }

        /// <summary>
        /// <see cref="VesselModel"/>
        /// </summary>
        public string VesselLoadCapacity { get; set; }

        /// <summary>
        /// Компания перевозчик
        /// </summary>
        public Guid CompanyPerId { get; set; }

        /// <summary>
        /// Компания заказчик
        /// </summary>
        public Guid CompanyZakazchikId { get; set; }

        /// <summary>
        /// Пост
        /// </summary>
        public string Responsible_cargoName { get; set; }
    }
}
