using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Documenti"/>
    /// </summary>
    public interface IDocumentiReadRepository
    {
        /// <summary>
        /// Получить список всех сотрудников <see cref="Documenti"/>
        /// </summary>
        Task<List<Documenti>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Documenti"/> по id
        /// </summary>
        Task<Documenti?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Documenti"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Documenti>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Проверить есть ли <see cref="Documenti"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
