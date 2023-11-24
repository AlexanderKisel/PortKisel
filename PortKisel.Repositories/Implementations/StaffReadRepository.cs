using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class StaffReadRepository : IStaffReadRepository, IRepositoryAnchor
    {
        private readonly IPortContext context;

        public StaffReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<Staff>> IStaffReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Staffs.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Post)
                .ToList());

        Task<Staff?> IStaffReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Staffs.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Staff>> IStaffReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => Task.FromResult(context.Staffs.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Post)
                .ToDictionary(x => x.Id));
    }
}