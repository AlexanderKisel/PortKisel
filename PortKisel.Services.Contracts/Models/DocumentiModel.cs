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
        public CargoModel? CargoName { get; set; }

        /// <summary>
        /// Вес груза
        /// </summary>
        public CargoModel? Weight { get; set; }

        /// <summary>
        /// <see cref="Vessel"/>
        /// </summary>
        public VesselModel? VesselName { get; set; }

        /// <summary>
        /// Компания перевозчик
        /// </summary>
        public CompanyPerModel? CompanyPerName { get; set; }

        /// <summary>
        /// Компания заказчик
        /// </summary>
        public CompanyZakazchikModel? CompanyZakazchikName { get; set; }

        /// <summary>
        /// Пост
        /// </summary>
        public PostModels? Responsible_cargo { get; set; }
    }
}
