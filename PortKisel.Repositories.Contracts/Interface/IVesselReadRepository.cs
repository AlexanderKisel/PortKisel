using PortKisel.Context.Contracts.Models;

namespace PortKisel.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Vessel"/>
    /// </summary>
    public interface IVesselReadRepository
    {
        /// <summary>
        /// Получить список всех сотрудников <see cref="Vessel"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Vessel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Vessel"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Vessel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
