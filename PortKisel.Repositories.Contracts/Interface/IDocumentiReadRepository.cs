using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Documenti"/>
    /// </summary>
    public interface IDocumentiReadRepository
    {
        /// <summary>
        /// Получить список всех сотрудников <see cref="Documenti"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Documenti>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Documenti"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Documenti?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
