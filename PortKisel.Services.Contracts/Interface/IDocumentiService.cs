using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Contracts.Interface
{
    public interface IDocumentiService
    {
        /// <summary>
        /// Получить список всех <see cref="DocumentiModel"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<DocumentiModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DocumentiModel"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DocumentiModel?> GetByAsync(Guid id, CancellationToken cancellationToken);
    }
}
