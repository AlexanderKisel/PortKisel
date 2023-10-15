using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Cargo"/>
    /// </summary>
    public interface ICargoReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Cargo"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Cargo>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Cargo"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Cargo?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
