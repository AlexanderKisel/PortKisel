using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class CompanyZakazchikReadRepository : ICompanyZakazchikReadRepository, IRepositoryAnchor
    {
        private readonly IPortContext context;

        public CompanyZakazchikReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<CompanyZakazchik>> ICompanyZakazchikReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyZakazchiks.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<CompanyZakazchik?> ICompanyZakazchikReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyZakazchiks.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, CompanyZakazchik>> ICompanyZakazchikReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyZakazchiks.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionary(x => x.Id));
    }
}
