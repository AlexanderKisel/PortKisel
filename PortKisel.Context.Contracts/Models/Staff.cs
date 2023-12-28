using PortKisel.Context.Contracts.Enums;

namespace PortKisel.Context.Contracts.Models
{
    public class Staff : BaseAuditEntity
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; } = string.Empty;

        /// <summary>
        /// Должность
        /// </summary>
        public Posts Post { get; set; } = Posts.None;

        /// <summary>
        /// Список документов
        /// </summary>
        public IEnumerable<Documenti> Documenti { get; set; }
    }
}
