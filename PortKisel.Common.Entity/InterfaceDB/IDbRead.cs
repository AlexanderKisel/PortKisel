using PortKisel.Common.Entity.EntityInterface;

namespace PortKisel.Common.Entity.InterfaceDB
{
    /// <summary>
    /// Интерфейс получения
    /// </summary>
    public interface IDbRead
    {
        /// <summary>
        /// Предоставляет функциональные возможности для выполнения запросов
        /// </summary>
        IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
    }
}
