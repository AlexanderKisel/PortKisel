using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Contracts.Interface
{
    public interface ICompanyPerService
    {
        /// <summary>
        /// Получить список всех <see cref="CompanyPerModel"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<CompanyPerModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyPer"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CompanyPerModel?> GetByAsync(Guid id, CancellationToken cancellationToken);
    }
}
