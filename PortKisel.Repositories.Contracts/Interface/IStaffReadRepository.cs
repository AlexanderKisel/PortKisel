using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Staff"/>
    /// </summary>
    public interface IStaffReadRepository
    {
        /// <summary>
        /// Получить список всех сотрудников <see cref="Staff"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Staff>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Staff"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Staff?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}