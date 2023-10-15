using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class CompanyPerService : ICompanyPerService
    {
        private readonly ICompanyPerReadRepository companyPerReadRepository;

        public CompanyPerService(ICompanyPerReadRepository companyPerReadRepository)
        {
            this.companyPerReadRepository = companyPerReadRepository;
        }

        async Task<IEnumerable<CompanyPerModel>> ICompanyPerService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await companyPerReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new CompanyPerModel
            {
                Id = x.Id,
                CompanyPerName = x.CompanyPerName,
                CompanyPerDescription = x.CompanyPerDescription,
            });
        }

        async Task<CompanyPerModel?> ICompanyPerService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await companyPerReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new CompanyPerModel
            {
                Id = item.Id,
                CompanyPerName = item.CompanyPerName,
                CompanyPerDescription = item.CompanyPerDescription,
            };
        }
    }
}
