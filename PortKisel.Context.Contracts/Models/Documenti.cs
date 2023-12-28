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
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssaedAt { get; set; }

        /// <summary>
        /// <see cref="Cargo"/>
        /// В документе описывается определенный груз
        /// </summary>
        public Guid CargoId { get; set; }

        /// <summary>
        /// Груз
        /// </summary>
        public Cargo? Cargo { get; set; }

        /// <summary>
        /// <see cref="Vessel"/>
        /// В документе описывается судно
        /// </summary>
        public Guid VesselId { get; set; }

        /// <summary>
        /// Судно
        /// </summary>
        public Vessel? Vessel { get; set; }

        /// <summary>
        /// Ответственный за груз
        /// </summary>
        public Guid? Responsible_cargoId { get; set; }

        /// <summary>
        /// Работник
        /// </summary>
        public Staff? Staff { get; set; }

        /// <summary>
        /// Ответственный за груз
        /// </summary>
        public Posts? Responsible_cargo { get; set; }
    }
}
