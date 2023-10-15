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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<CompanyZakazchik>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyZakazchik"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CompanyZakazchik?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
