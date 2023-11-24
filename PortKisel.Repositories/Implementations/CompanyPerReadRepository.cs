using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class CompanyPerReadRepository : ICompanyPerReadRepository, IRepositoryAnchor
    {
        private readonly IPortContext context;

        public CompanyPerReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<CompanyPer>> ICompanyPerReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyPers.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<CompanyPer?> ICompanyPerReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyPers.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, CompanyPer>> ICompanyPerReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyPers.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionary(x => x.Id));
    }
}
