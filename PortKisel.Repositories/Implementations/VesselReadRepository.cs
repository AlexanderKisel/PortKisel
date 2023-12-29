using Microsoft.EntityFrameworkCore;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Common.Entity.Repositories;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class VesselReadRepository : IVesselReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public VesselReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<List<Vessel>> IVesselReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Vessel>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Description)
            .ThenBy(x => x.LoadCapacity)
            .ToListAsync(cancellationToken);

        Task<Vessel?> IVesselReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Vessel>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Vessel>> IVesselReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Vessel>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Description)
                .ThenBy(x => x.LoadCapacity)
                .ToDictionaryAsync(key => key.Id, cancellationToken);
    }
}
