using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Contracts.Interface
{
    public interface IStaffService
    {
        /// <summary>
        /// Получить список всех <see cref="StaffModel"/>
        /// </summary>
        Task<IEnumerable<StaffModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="StaffModel"/> по id
        /// </summary>
        Task<StaffModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавление новый груза
        /// </summary>
        Task<StaffModel> AddAsync(StaffRequestModel staff, CancellationToken cancellationToken);

        /// <summary>
        /// Изменяет существующий груз
        /// </summary>
        Task<StaffModel> UpdateAsync(StaffRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий груз
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
