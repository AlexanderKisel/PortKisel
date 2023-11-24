using AutoMapper;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class VesselService : IVesselService, IServiceAnchor
    {
        private readonly IVesselReadRepository vesselReadRepository;
        private readonly ICompanyPerReadRepository companyPerReadRepository;
        private readonly IMapper mapper;

        public VesselService(IVesselReadRepository vesselReadRepository,
            ICompanyPerReadRepository companyPerReadRepository,
            IMapper mapper)
        {
            this.vesselReadRepository = vesselReadRepository;
            this.companyPerReadRepository = companyPerReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<VesselModel>> IVesselService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await vesselReadRepository.GetAllAsync(cancellationToken);
            var companyPers = await companyPerReadRepository.GetByIdsAsync(result.Select(x => x.CompanyPerId).Distinct(), cancellationToken);
            var listVessel = new List<VesselModel>();
            foreach (var vessel in result)
            {
                if(!companyPers.TryGetValue(vessel.CompanyPerId, out var companyPer))
                {
                    continue;
                }
                var ves = mapper.Map<VesselModel>(vessel);
                ves.CompanyPer = mapper.Map<CompanyPerModel>(companyPer);
                listVessel.Add(ves);
            }
            return listVessel;
        }

        async Task<VesselModel?> IVesselService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await vesselReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            var companyPer = await companyPerReadRepository.GetByIdAsync(item.CompanyPerId, cancellationToken);
            var vessel = mapper.Map<VesselModel>(item);
            vessel.CompanyPer = mapper.Map<CompanyPerModel>(companyPer);

            return vessel;
        }
    }
}
