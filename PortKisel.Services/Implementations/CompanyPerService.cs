using AutoMapper;
using PortKisel.Services.Anchors;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class CompanyPerService : ICompanyPerService, IServiceAnchor
    {
        private readonly ICompanyPerReadRepository companyPerReadRepository;
        private readonly IMapper mapper;

        public CompanyPerService(ICompanyPerReadRepository companyPerReadRepository, IMapper mapper)
        {
            this.companyPerReadRepository = companyPerReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<CompanyPerModel>> ICompanyPerService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await companyPerReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<CompanyPerModel>>(result);
        }

        async Task<CompanyPerModel?> ICompanyPerService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await companyPerReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<CompanyPerModel>(item);
        }
    }
}
