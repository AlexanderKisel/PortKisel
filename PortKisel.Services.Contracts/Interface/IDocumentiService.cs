using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Contracts.Interface
{
    public interface IDocumentiService
    {
        /// <summary>
        /// Получить список всех <see cref="DocumentiModel"/>
        /// </summary>
        Task<IEnumerable<DocumentiModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DocumentiModel"/> по id
        /// </summary>
        Task<DocumentiModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление новый груза
        /// </summary>
        Task<DocumentiModel> AddAsync(DocumentiRequestModel documenti, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет существующий груз
        /// </summary>
        Task<DocumentiModel> UpdateAsync(DocumentiRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий груз
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
