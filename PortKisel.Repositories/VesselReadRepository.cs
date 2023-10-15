using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories
{
    public class VesselReadRepository : IVesselReadRepository
    {
        private readonly IPortContext context;

        public VesselReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<Vessel>> IVesselReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Vessels.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.NameVessel)
                .ToList());

        Task<Vessel?> IVesselReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Vessels.FirstOrDefault(x => x.Id == id));
    }
}
