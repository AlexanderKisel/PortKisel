namespace PortKisel.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит удаление сущности
    /// </summary>
    public interface IEntityAuditDeleted
    {
        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
