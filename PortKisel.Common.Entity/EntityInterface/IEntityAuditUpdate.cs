namespace PortKisel.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит изменения сущности
    /// </summary>
    public interface IEntityAuditUpdate
    {
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTimeOffset UpdateAt { get; set; }

        /// <summary>
        /// Кто изменил
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
