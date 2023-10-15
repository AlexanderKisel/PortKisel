using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories
{
    public class CompanyZakazchikReadRepository : ICompanyZakazchikReadRepository
    {
        private readonly IPortContext context;

        public CompanyZakazchikReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<CompanyZakazchik>> ICompanyZakazchikReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyZakazchiks.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.CompanyZakazchikName)
                .ToList());

        Task<CompanyZakazchik?> ICompanyZakazchikReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.CompanyZakazchiks.FirstOrDefault(x => x.Id == id));
    }
}
