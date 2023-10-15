using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Contracts.Interface
{
    public interface IVesselService
    {
        /// <summary>
        /// Получить список всех <see cref="VesselModel"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<VesselModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="VesselModel"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<VesselModel?> GetByAsync(Guid id, CancellationToken cancellationToken);
    }
}
