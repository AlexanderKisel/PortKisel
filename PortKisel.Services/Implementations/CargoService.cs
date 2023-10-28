using AutoMapper;
using PortKisel.Services.Anchors;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class CargoService : ICargoService, IServiceAnchor
    {
        private readonly ICargoReadRepository cargoReadRepository;
        private readonly IMapper mapper;

        public CargoService(ICargoReadRepository cargoReadRepository, IMapper mapper)
        {
            this.cargoReadRepository = cargoReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<CargoModel>> ICargoService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await cargoReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<CargoModel>>(result);
        }

        async Task<CargoModel?> ICargoService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await cargoReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<CargoModel>(item);
        }
    }
}
