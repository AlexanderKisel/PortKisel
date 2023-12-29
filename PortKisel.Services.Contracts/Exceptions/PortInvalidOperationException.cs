namespace PortKisel.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибка выполнения операции
    /// </summary>
    public class PortInvalidOperationException : PortException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PortInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public PortInvalidOperationException(string message)
            : base(message) { }
    }
}
