using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="CompanyZakazchik"/>
    /// </summary>
    public interface ICompanyZakazchikReadRepository
    {
        /// <summary>
        /// Получить список всех сотрудников <see cref="CompanyZakazchik"/>
        /// </summary>
        Task<List<CompanyZakazchik>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyZakazchik"/> по id
        /// </summary>
        Task<CompanyZakazchik?> GetByIdAsync(Guid id, CancellationToken cancellationToken);


        /// <summary>
        /// Получить список всех компаний по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, CompanyZakazchik>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="CompanyZakazchik"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="CompanyZakazchik"/> в коллеции
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
