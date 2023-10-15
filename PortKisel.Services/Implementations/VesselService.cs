using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class VesselService : IVesselService
    {
        private readonly IVesselReadRepository vesselReadRepository;

        public VesselService(IVesselReadRepository vesselReadRepository)
        {
            this.vesselReadRepository = vesselReadRepository;
        }
        async Task<IEnumerable<VesselModel>> IVesselService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await vesselReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new VesselModel
            {
                Id = x.Id,
                NameVessel = x.NameVessel,
                Description = x.Description,
                CompanyPerId = x.CompanyPerId,
                LoadCapacity = x.LoadCapacity,
            });
        }

        async Task<VesselModel?> IVesselService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await vesselReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new VesselModel
            {
                Id = item.Id,
                NameVessel = item.NameVessel,
                Description = item.Description,
                CompanyPerId = item.CompanyPerId,
                LoadCapacity = item.LoadCapacity,
            };
        }
    }
}
