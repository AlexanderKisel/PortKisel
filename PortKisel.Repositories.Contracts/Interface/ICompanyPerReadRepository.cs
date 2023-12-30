using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="CompanyPer"/>
    /// </summary>
    public interface ICompanyPerReadRepository
    {
        /// <summary>
        /// получить список всех <see cref="CompanyPer"/>
        /// </summary>
        Task<List<CompanyPer>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyPer"/> по идентификатору
        /// </summary>
        Task<CompanyPer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="CompanyPer"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, CompanyPer>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="CompanyPer"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
