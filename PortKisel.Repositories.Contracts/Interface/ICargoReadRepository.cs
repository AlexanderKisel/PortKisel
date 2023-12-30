using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Cargo"/>
    /// </summary>
    public interface ICargoReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Cargo"/>
        /// </summary>
        Task<List<Cargo>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Cargo"/> по id
        /// </summary>
        Task<Cargo?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Cargo"/>
        /// </summary>
        Task<Dictionary<Guid, Cargo>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка есть <see cref="Cargo"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
