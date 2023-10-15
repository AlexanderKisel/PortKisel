using PortKisel.Context.Contracts.Enums;

namespace PortKisel.Context.Contracts.Models
{
    /// <summary>
    /// Документы
    /// </summary>
    public class Documenti : BaseAuditEntity
    {
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
        public Posts Posts { get; set; }
    }
}
