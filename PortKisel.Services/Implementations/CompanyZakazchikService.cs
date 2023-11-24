using AutoMapper;
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
            var listCompanyZakazchik = new List<CompanyZakazchikModel>();
            foreach(var companyZakazchik in result)
            {
                var companyZ = mapper.Map<CompanyZakazchikModel>(companyZakazchik);
                listCompanyZakazchik.Add(companyZ);
            }
            return listCompanyZakazchik;
        }

        async Task<CompanyZakazchikModel?> ICompanyZakazchikService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await companyZakazchikReadRepository.GetByIdAsync(id, cancellationToken);
            if(item == null)
            {
                return null;
            }
            var companyZ = mapper.Map<CompanyZakazchikModel>(item);

            return companyZ;
        }
    }
}
