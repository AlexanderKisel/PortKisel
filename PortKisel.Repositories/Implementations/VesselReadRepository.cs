using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class VesselReadRepository : IVesselReadRepository, IRepositoryAnchor
    {
        private readonly IPortContext context;

        public VesselReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<Vessel>> IVesselReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Vessels.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Vessel?> IVesselReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Vessels.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Vessel>> IVesselReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => Task.FromResult(context.Vessels.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionary(x => x.Id));
    }
}
