namespace PortKisel.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемый ресурс не найден
    /// </summary>
    public class PortNotFoundException : PortException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PortNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public PortNotFoundException(string message)
            : base(message) { }
    }
}
