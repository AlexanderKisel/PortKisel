using AutoMapper;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Repositories.Implementations;
using PortKisel.Services.Contracts.Exceptions;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Implementations
{
    public class DocumentiService : IDocumentiService, IServiceAnchor
    {
        private readonly IDocumentiReadRepository documentiReadRepository;
        private readonly ICargoReadRepository cargoReadRepository;
        private readonly IVesselReadRepository vesselReadRepository;
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IMapper mapper;

        public DocumentiService(IDocumentiReadRepository documentiReadRepository,
            ICargoReadRepository cargoReadRepository,
            IVesselReadRepository vesselReadRepository,
            IStaffReadRepository staffReadRepository,
            IMapper mapper)
        {
            this.documentiReadRepository = documentiReadRepository;
            this.cargoReadRepository = cargoReadRepository;
            this.vesselReadRepository = vesselReadRepository;
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<DocumentiModel>> IDocumentiService.GetAllAsync(CancellationToken cancellationToken)
        {
            var results = await documentiReadRepository.GetAllAsync(cancellationToken);
            var vessels = await vesselReadRepository.GetByIdsAsync(results.Select(x => x.VesselId).Distinct(), cancellationToken);
            var cargos = await cargoReadRepository.GetByIdsAsync(results.Select(x => x.CargoId).Distinct(), cancellationToken);
            var staffIds = results.Where(x => x.Responsible_cargoId.HasValue)
                .Select(x => x.Responsible_cargoId!.Value)
                .Distinct();

            var listDocumenti = new List<DocumentiRequestModel>();
            foreach (var document in results)
            {
                if (!vessels.TryGetValue(document.VesselId, out var vessel))
                {
                    continue;
                }
                if (!!cargos.TryGetValue(document.VesselId, out var cargo))
                {
                    continue;
                }
                var doc = mapper.Map<DocumentiRequestModel>(document);
                doc.Vessel = mapper.Map<VesselRequestModel>(vessel);
                doc.Cargo = mapper.Map<CargoRequestModel>(cargo);

                listDocumenti.Add(doc);
            }
            return listDocumenti;
        }

        async Task<DocumentiRequestModel?> IDocumentiService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await documentiReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var cargo = await cargoReadRepository.GetByIdAsync(id, cancellationToken);
            var vessel = await vesselReadRepository.GetByIdAsync(id, cancellationToken);
            var staff = await staffReadRepository.GetByIdAsync(id, cancellationToken);
            var doc = mapper.Map<DocumentiRequestModel>(item);
            doc.Vessel = mapper.Map<VesselRequestModel>(vessel);
            doc.Cargo = mapper.Map<CargoRequestModel>(cargo);
            doc.Responsible_cargo = mapper.Map<StaffRequestModel>(staff);

            return doc;
        }
    }
}
