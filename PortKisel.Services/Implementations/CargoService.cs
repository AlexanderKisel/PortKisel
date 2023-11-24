using AutoMapper;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class CargoService : ICargoService, IServiceAnchor
    {
        private readonly ICargoReadRepository cargoReadRepository;
        private readonly ICompanyZakazchikReadRepository companyZakazchikReadRepository;
        private readonly IMapper mapper;

        public CargoService(ICargoReadRepository cargoReadRepository,
            ICompanyZakazchikReadRepository companyZakazchikReadRepository,
            IMapper mapper)
        {
            this.cargoReadRepository = cargoReadRepository;
            this.companyZakazchikReadRepository = companyZakazchikReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<CargoModel>> ICargoService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await cargoReadRepository.GetAllAsync(cancellationToken);
            var companyZakazchiks = await companyZakazchikReadRepository.GetByIdsAsync(result.Select(x => x.CompanyZakazchikId).Distinct(), cancellationToken);
            var listCargo = new List<CargoModel>();
            foreach(var cargo in result)
            {
                if(!companyZakazchiks.TryGetValue(cargo.CompanyZakazchikId, out var companyZakazchik))
                {
                    continue;
                }
                var car = mapper.Map<CargoModel>(cargo);
                car.CompanyZakazchik = mapper.Map<CompanyZakazchikModel>(companyZakazchik);
                listCargo.Add(car);
            }

            return listCargo;
        }

        async Task<CargoModel?> ICargoService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await cargoReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var companyZakazchik = await companyZakazchikReadRepository.GetByIdAsync(id, cancellationToken);
            var car = mapper.Map<CargoModel>(item);
            car.CompanyZakazchik = mapper.Map<CompanyZakazchikModel>(companyZakazchik);

            return car;
        }
    }
}
