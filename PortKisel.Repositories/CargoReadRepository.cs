using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories
{
    public class CargoReadRepository : ICargoReadRepository
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
    }
}
