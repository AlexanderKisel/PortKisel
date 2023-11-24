using PortKisel.Services.Contracts.Models.Enums;

namespace PortKisel.Services.Contracts.Models
{
    /// <summary>
    /// Модель сотрудников
    /// </summary>
    public class StaffModel
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
        public PostModels Post { get; set; } = PostModels.None;
    }
}
