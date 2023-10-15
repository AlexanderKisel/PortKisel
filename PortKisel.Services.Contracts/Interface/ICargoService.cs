using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Contracts.Interface
{
    public interface ICargoService
    {
        /// <summary>
        /// Получить список всех <see cref="CargoModel"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<CargoModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CargoModel"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CargoModel?> GetByAsync(Guid id, CancellationToken cancellationToken);
    }
}
