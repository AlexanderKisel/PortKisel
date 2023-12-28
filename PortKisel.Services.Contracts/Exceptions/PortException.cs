namespace PortKisel.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс
    /// </summary>
    public abstract class PortException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PortException"/> без параметров
        /// </summary>
        protected PortException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PortException"/> с указанием параметров
        /// </summary>
        protected PortException(string message) : base(message) { }
    }
}
