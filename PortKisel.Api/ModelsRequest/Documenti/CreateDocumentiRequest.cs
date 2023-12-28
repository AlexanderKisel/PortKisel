namespace PortKisel.Api.ModelsRequest.Documenti
{
    public class CreateDocumentiRequest
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
        /// <see cref="Vessel"/>
        /// В документе описывается судно
        /// </summary>
        public Guid VesselId { get; set; }

        /// <summary>
        /// Ответственный за груз
        /// </summary>
        public Guid StaffId { get; set; }
    }
}
