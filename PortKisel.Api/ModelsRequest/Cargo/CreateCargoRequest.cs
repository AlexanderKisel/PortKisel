namespace PortKisel.Api.ModelsRequest.Cargo
{
    public class CreateCargoRequest
    {
        /// <summary>
        /// Название груза
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание грузы
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Масса груза
        /// </summary>
        public string Weight { get; set; } = string.Empty;

        /// <summary>
        /// <see cref="CompanyZakazchik"/>
        /// </summary>
        public Guid CompanyZakazchikId { get; set; }
    }
}
