using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Contracts.Interface
{
    public interface IVesselService
    {
        /// <summary>
        /// Получить список всех <see cref="VesselModel"/>
        /// </summary>
        Task<IEnumerable<VesselModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="VesselModel"/> по id
        /// </summary>
        Task<VesselModel?> GetByAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление новый груза
        /// </summary>
        Task<VesselModel> AddAsync(VesselRequestModel vessel, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет существующий груз
        /// </summary>
        Task<VesselModel> UpdateAsync(VesselRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий груз
        /// </summary>
        Task<VesselModel> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
