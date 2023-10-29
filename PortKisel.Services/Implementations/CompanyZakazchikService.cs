using AutoMapper;
using PortKisel.Services.Anchors;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class CompanyZakazchikService : ICompanyZakazchikService, IServiceAnchor
    {
        private readonly ICompanyZakazchikReadRepository companyZakazchikReadRepository;
        private readonly IMapper mapper;
        
        public CompanyZakazchikService(ICompanyZakazchikReadRepository companyZakazchikReadRepository, IMapper mapper)
        {
            this.companyZakazchikReadRepository = companyZakazchikReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<CompanyZakazchikModel>> ICompanyZakazchikService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await companyZakazchikReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<CompanyZakazchikModel>>(result);
        }

        async Task<CompanyZakazchikModel?> ICompanyZakazchikService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await companyZakazchikReadRepository.GetByIdAsync(id, cancellationToken);
            if(item == null)
            {
                return null;
            }

            return mapper.Map<CompanyZakazchikModel>(item);
        }
    }
}
