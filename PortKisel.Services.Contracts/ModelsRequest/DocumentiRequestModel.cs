namespace PortKisel.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Модель документов
    /// </summary>
    public class DocumentiRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public CargoRequestModel Cargo { get; set; }


        /// <summary>
        /// <see cref="Vessel"/>
        /// В документе описывается судно
        /// </summary>
        public VesselRequestModel Vessel { get; set; }

        /// <summary>
        /// Ответственный за груз
        /// </summary>
        public StaffRequestModel? Responsible_cargo { get; set; }
    }
}
