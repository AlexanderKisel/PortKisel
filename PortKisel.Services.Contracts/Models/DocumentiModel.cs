using PortKisel.Services.Contracts.Models.Enums;

namespace PortKisel.Services.Contracts.Models
{
    /// <summary>
    /// Модель документов
    /// </summary>
    public class DocumentiModel
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
        /// <see cref="Cargo"/>
        /// </summary>
        public Guid CargoId { get; set; }


        /// <summary>
        /// <see cref="Vessel"/>
        /// </summary>
        public Guid VesselId { get; set; }

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
        public PostModels Posts { get; set; }
    }
}
