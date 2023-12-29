using AutoMapper;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Exceptions;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Implementations
{
    public class CargoService : ICargoService, IServiceAnchor
    {
        private readonly ICargoReadRepository cargoReadRepository;
        private readonly ICompanyZakazchikReadRepository companyZakazchikReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICargoWriteRepository cargoWriteRepository;

        public CargoService(ICargoReadRepository cargoReadRepository,
            ICompanyZakazchikReadRepository companyZakazchikReadRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICargoWriteRepository cargoWriteRepository)
        {
            this.cargoReadRepository = cargoReadRepository;
            this.companyZakazchikReadRepository = companyZakazchikReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.cargoWriteRepository = cargoWriteRepository;
        }

        async Task<IEnumerable<CargoModel>> ICargoService.GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            var cargos = await cargoReadRepository.GetAllAsync(cancellationToken);
            var companyZakazchikIds = cargos.Select(x => x.CompanyZakazchikId).Distinct();

            var companyZakazchikDictionary = await companyZakazchikReadRepository.GetByIdsAsync(companyZakazchikIds, cancellationToken);

            var listCargoModel = new List<CargoModel>();
            foreach (var cargo in cargos)
            {
                if(!companyZakazchikDictionary.TryGetValue(cargo.CompanyZakazchikId, out var companyZakazchik))
                {
                    continue;
                }
                var cargoMap = mapper.Map<CargoModel>(cargo);
                cargoMap.CompanyZakazchik = mapper.Map<CompanyZakazchikModel>(companyZakazchik);
                listCargoModel.Add(cargoMap);
            }

            return listCargoModel;
        }

        async Task<CargoModel?> ICargoService.GetByIdAsync(System.Guid id, System.Threading.CancellationToken cancellationToken)
        {
            var item = await cargoReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var companyZakazchik = await companyZakazchikReadRepository.GetByIdAsync(item.CompanyZakazchikId, cancellationToken);
            var cargo = mapper.Map<CargoModel>(item);
            cargo.CompanyZakazchik = companyZakazchik != null
                ? mapper.Map<CompanyZakazchikModel>(companyZakazchik)
                : null;
            return cargo;
        }

        async Task<CargoModel> ICargoService.AddAsync(CargoRequestModel cargo, CancellationToken cancellationToken)
        {
            var item = new Cargo
            {
                Id = Guid.NewGuid(),
                Name = cargo.Name,
                Description = cargo.Description,
                Weight = cargo.Weight,
                CompanyZakazchikId = cargo.CompanyZakazchikId
            };

            cargoWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CargoModel>(item);
        }

        async Task<CargoModel> ICargoService.UpdateAsync(CargoRequestModel source, CancellationToken cancellationToken)
        {
            var targetCargo = await cargoReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetCargo == null)
            {
                throw new PortEntityNotFoundException<Cargo>(source.Id);
            }
            targetCargo.Name = source.Name;
            targetCargo.Description = source.Description;
            targetCargo.Weight = source.Weight;

            var companyZakazchik = await companyZakazchikReadRepository.GetByIdAsync(source.CompanyZakazchikId, cancellationToken);
            targetCargo.CompanyZakazchikId = companyZakazchik.Id;

            cargoWriteRepository.Update(targetCargo);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CargoModel>(targetCargo);
        }

        async Task ICargoService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCargo = await cargoReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetCargo == null)
            {
                throw new PortEntityNotFoundException<Cargo>(id);
            }
            if (targetCargo.DeletedAt.HasValue)
            {
                throw new PortInvalidOperationException($"Документ с идентификатором {id} уже удален");
            }

            cargoWriteRepository.Delete(targetCargo);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
