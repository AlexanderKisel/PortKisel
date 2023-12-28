using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Contracts.Interface
{
    public interface ICompanyPerService
    {
        /// <summary>
        /// Получить список всех <see cref="CompanyPerModel"/>
        /// </summary>
        Task<IEnumerable<CompanyPerModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyPerModel"/> по id
        /// </summary>
        Task<CompanyPerModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление новую компанию перевозчика
        /// </summary>
        Task<CompanyPerModel> AddAsync(CompanyPerRequestModel companyPer, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет существующию компанию перевозчика
        /// </summary>
        Task<CompanyPerModel> UpdateAsync(CompanyPerRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующию компанию перевозчика
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
