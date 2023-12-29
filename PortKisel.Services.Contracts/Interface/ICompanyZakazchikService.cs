using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Contracts.Interface
{
    public interface ICompanyZakazchikService
    {
        /// <summary>
        /// Получить список всех <see cref="CompanyZakazchikModel"/>
        /// </summary>
        Task<IEnumerable<CompanyZakazchikModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyZakazchikModel"/> по id
        /// </summary>
        Task<CompanyZakazchikModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление новый груза
        /// </summary>
        Task<CompanyZakazchikModel> AddAsync(CompanyZakazchikRequestModel companyZakazchik, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет существующий груз
        /// </summary>
        Task<CompanyZakazchikModel> UpdateAsync(CompanyZakazchikRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий груз
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
