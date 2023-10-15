namespace PortKisel.Models
{
    /// <summary>
    /// Модель ответа сущности сотрудники
    /// </summary>
    public class StaffResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; } = string.Empty;
        
        /// <summary>
        /// Должность
        /// </summary>
        public string Posts { get; set; } = string.Empty;
    }
}
