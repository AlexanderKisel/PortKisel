namespace PortKisel.Api.ModelsRequest.Cargo
{
    public class EditCargoRequest : CreateCargoRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
