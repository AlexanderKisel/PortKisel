using Microsoft.EntityFrameworkCore;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Common.Entity.Repositories;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;


namespace PortKisel.Repositories.Implementations
{
    public class CargoReadRepository : ICargoReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public CargoReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<List<Cargo>> ICargoReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Cargo>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Description)
            .ThenBy(x => x.Weight)
            .ToListAsync(cancellationToken);

        Task<Cargo?> ICargoReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Cargo>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Cargo>> ICargoReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Cargo>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Description)
                .ThenBy(x => x.Weight)
                .ToDictionaryAsync(key => key.Id, cancellationToken);
    }
}
