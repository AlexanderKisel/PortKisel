using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;

namespace PortKisel.Services.Implementations
{
    public class CargoService : ICargoService
    {
        private readonly ICargoReadRepository cargoReadRepository;

        public CargoService(ICargoReadRepository cargoReadRepository)
        {
            this.cargoReadRepository = cargoReadRepository;
        }

        async Task<IEnumerable<CargoModel>> ICargoService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await cargoReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new CargoModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Weight = x.Weight,
                CompanyZakazchikId = x.CompanyZakazchikId,
            });
        }

        async Task<CargoModel?> ICargoService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await cargoReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new CargoModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Weight = item.Weight,
                CompanyZakazchikId = item.CompanyZakazchikId,
            };
        }
    }
}
