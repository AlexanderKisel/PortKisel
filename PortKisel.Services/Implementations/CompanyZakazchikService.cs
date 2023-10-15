using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class CompanyZakazchikService : ICompanyZakazchikService
    {
        private readonly ICompanyZakazchikReadRepository companyZakazchikReadRepository;
        
        public CompanyZakazchikService(ICompanyZakazchikReadRepository companyZakazchikReadRepository)
        {
            this.companyZakazchikReadRepository = companyZakazchikReadRepository;
        }

        async Task<IEnumerable<CompanyZakazchikModel>> ICompanyZakazchikService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await companyZakazchikReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new CompanyZakazchikModel
            {
                Id = x.Id,
                CompanyZakazchikName = x.CompanyZakazchikName,
                CompanyZakazchikDescription = x.CompanyZakazchikDescription,
            });
        }

        async Task<CompanyZakazchikModel?> ICompanyZakazchikService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await companyZakazchikReadRepository.GetByIdAsync(id, cancellationToken);
            if(item == null)
            {
                return null;
            }

            return new CompanyZakazchikModel
            {
                Id = item.Id,
                CompanyZakazchikName = item.CompanyZakazchikName,
                CompanyZakazchikDescription = item.CompanyZakazchikDescription,
            };
        }
    }
}
