using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Contracts.Interface
{
    public interface ICargoService
    {
        /// <summary>
        /// Получить список всех <see cref="CargoModel"/>
        /// </summary>
        Task<IEnumerable<CargoModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CargoModel"/> по id
        /// </summary>
        Task<CargoModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление новый груза
        /// </summary>
        Task<CargoModel> AddAsync(CargoRequestModel cargo, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет существующий груз
        /// </summary>
        Task<CargoModel> UpdateAsync(CargoRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий груз
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
