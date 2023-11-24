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
        Task<List<Vessel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Vessel"/> по id
        /// </summary>
        Task<Vessel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        
        /// <summary>
        /// Получить список <see cref="Vessel"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Vessel>>GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
