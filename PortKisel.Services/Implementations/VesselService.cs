using AutoMapper;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Enums;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Repositories.Implementations;
using PortKisel.Services.Contracts.Exceptions;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Implementations
{
    public class VesselService : IVesselService, IServiceAnchor
    {
        private readonly IVesselReadRepository vesselReadRepository;
        private readonly ICompanyPerReadRepository companyPerReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IVesselWriteRepository vesselWriteRepository;

        public VesselService(IVesselReadRepository vesselReadRepository,
            ICompanyPerReadRepository companyPerReadRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IVesselWriteRepository vesselWriteRepository)
        {
            this.vesselReadRepository = vesselReadRepository;
            this.companyPerReadRepository = companyPerReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.vesselWriteRepository = vesselWriteRepository;
        }

        async Task<IEnumerable<VesselModel>> IVesselService.GetAllAsync(CancellationToken cancellationToken)
        {
            var vessels = await vesselReadRepository.GetAllAsync(cancellationToken);
            var companyPerIds = vessels.Select(x => x.CompanyPerId).Distinct();

            var companyPerDictionary = await companyPerReadRepository.GetByIdsAsync(companyPerIds, cancellationToken);

            var listVesselModel = new List<VesselModel>();
            foreach (var vessel in vessels)
            {
                if (!companyPerDictionary.TryGetValue(vessel.CompanyPerId, out var companyPer))
                {
                    continue;
                }
                var vesselMap = mapper.Map<VesselModel>(vessel);
                vesselMap.CompanyPer = mapper.Map<CompanyPerModel>(companyPer);
                listVesselModel.Add(vesselMap);
            }

            return listVesselModel;
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
            vessel.CompanyPer = companyPer != null
                ? mapper.Map<CompanyPerModel>(companyPer)
                : null;

            return vessel;
        }

        Task<VesselModel> IVesselService.AddAsync(VesselRequestModel vessel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<VesselModel> IVesselService.UpdateAsync(VesselRequestModel source, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IVesselService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
