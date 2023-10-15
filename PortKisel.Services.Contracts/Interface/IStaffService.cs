using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Contracts.Interface
{
    public interface IStaffService
    {
        /// <summary>
        /// Получить список всех <see cref="StaffModel"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<StaffModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="StaffModel"/> по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<StaffModel?> GetByAsync(Guid id, CancellationToken cancellationToken);
    }
}
