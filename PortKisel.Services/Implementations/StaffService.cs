using AutoMapper;
using PortKisel.Services.Anchors;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class StaffService : IStaffService, IServiceAnchor
    {
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IMapper mapper;

        public StaffService(IStaffReadRepository staffReadRepository, IMapper mapper)
        {
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<StaffModel>> IStaffService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await staffReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<StaffModel>>(result);
        }

        async Task<StaffModel?> IStaffService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<StaffModel>(item);
        }
    }
}
