using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IStaffReadRepository staffReadRepository;

        public StaffService(IStaffReadRepository staffReadRepository)
        {
            this.staffReadRepository = staffReadRepository;
        }
        async Task<IEnumerable<StaffModel>> IStaffService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await staffReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new StaffModel
            {
                Id = x.Id,
                FIO = x.FIO,
                Posts = x.Post,
            });
        }

        async Task<StaffModel?> IStaffService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new StaffModel
            {
                Id = item.Id,
                FIO = item.FIO,
                Posts = item.Post,
            };
        }
    }
}
