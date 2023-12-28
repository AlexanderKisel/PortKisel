using AutoMapper;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.ModelsRequest;

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
        async Task<IEnumerable<StaffRequestModel>> IStaffService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await staffReadRepository.GetAllAsync(cancellationToken);
            var listStaff = new List<StaffRequestModel>();
            foreach (var staff in result)
            {
                var st = mapper.Map<StaffRequestModel>(staff);
                listStaff.Add(st);
            }
            return listStaff;
        }

        async Task<StaffRequestModel?> IStaffService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var st = mapper.Map<StaffRequestModel>(item);
            return st;
        }
    }
}
