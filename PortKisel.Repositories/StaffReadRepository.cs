using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories
{
    public class StaffReadRepository : IStaffReadRepository
    {
        private readonly IPortContext context;

        public StaffReadRepository(IPortContext context)
        {
            this.context = context;
        }
        
        Task<List<Staff>> IStaffReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Staff.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Post)
                .ToList());

        Task<Staff?> IStaffReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Staff.FirstOrDefault(x => x.Id == id));
    }
}