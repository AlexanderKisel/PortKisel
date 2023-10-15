using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories
{
    public class CompanyPerReadRepository : ICompanyPerReadRepository
    {
        private readonly IPortContext context;
        
        public CompanyPerReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<CompanyPer>> ICompanyPerReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => Task.FromResult(context.CompanyPers.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.CompanyPerName)
                .ToList());

        Task<CompanyPer?> ICompanyPerReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyPers.FirstOrDefault(x => x.Id == id));
    }
}
