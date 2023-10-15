using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="CompanyPer"/>
    /// </summary>
    public interface ICompanyPerReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="CompanyPer"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<CompanyPer>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyPer"/> по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CompanyPer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
