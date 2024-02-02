using PortKisel.Api.Models.Enums;
using PortKisel.Services.Contracts.Models.Enums;

namespace PortKisel.Api.Models
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
        public PostApi Post { get; set; }
    }
}
