using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class CargoReadRepository : ICargoReadRepository, IRepositoryAnchor
    {
        private readonly IPortContext context;

        public CargoReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<Cargo>> ICargoReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Cargos.Where(x => x.DeletedAt == null)
            .OrderBy(x => x.Name)
            .ToList());

        Task<Cargo?> ICargoReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Cargos.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Cargo>> ICargoReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => Task.FromResult(context.Cargos.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionary(x => x.Id));
    }
}
