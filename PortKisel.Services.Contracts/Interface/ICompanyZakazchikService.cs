using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Contracts.Interface
{
    public interface ICompanyZakazchikService
    {
        /// <summary>
        /// Получить список всех <see cref="CompanyZakazchikModel"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<CompanyZakazchikModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyZakazchik"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CompanyZakazchikModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
