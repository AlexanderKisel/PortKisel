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
    public class DocumentiService : IDocumentiService, IServiceAnchor
    {
        private readonly IDocumentiReadRepository documentiReadRepository;
        private readonly ICargoReadRepository cargoReadRepository;
        private readonly IVesselReadRepository vesselReadRepository;
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDocumentiWriteRepository documentiWriteRepository;

        public DocumentiService(IDocumentiReadRepository documentiReadRepository,
            ICargoReadRepository cargoReadRepository,
            IVesselReadRepository vesselReadRepository,
            IStaffReadRepository staffReadRepository,
            IMapper mapper,
            IDocumentiWriteRepository documentiWriteRepository,
            IUnitOfWork  unitOfWork)
        {
            this.documentiReadRepository = documentiReadRepository;
            this.cargoReadRepository = cargoReadRepository;
            this.vesselReadRepository = vesselReadRepository;
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
            this.documentiWriteRepository = documentiWriteRepository;
            this.unitOfWork = unitOfWork;
        }

        async Task<IEnumerable<DocumentiModel>> IDocumentiService.GetAllAsync(CancellationToken cancellationToken)
        {
            var documentis = await documentiReadRepository.GetAllAsync(cancellationToken);
            var cargoIds = documentis.Select(x => x.CargoId).Distinct();
            var vesselIds = documentis.Select(x => x.VesselId).Distinct();
            var staffIds = documentis.Select(x => x.StaffId).Distinct();

            var cargoDictionary = await cargoReadRepository.GetByIdsAsync(cargoIds, cancellationToken);
            var vesselDictionary = await vesselReadRepository.GetByIdsAsync(vesselIds, cancellationToken);
            var staffDictionary = await staffReadRepository.GetByIdsAsync(staffIds, cancellationToken);

            var listDocumentiModel = new List<DocumentiModel>();
            foreach(var documenti in documentis)
            {
                if(!cargoDictionary.TryGetValue(documenti.CargoId, out var cargo))
                {
                    continue;
                }
                if (!vesselDictionary.TryGetValue(documenti.VesselId, out var vessel))
                {
                    continue;
                }
                if (!staffDictionary.TryGetValue(documenti.StaffId, out var responsible_cargo))
                {
                    continue;
                }
                var documentiMap = mapper.Map<DocumentiModel>(documenti);
                documentiMap.Cargo = mapper.Map<CargoModel>(cargo);
                documentiMap.Vessel = mapper.Map<VesselModel>(vessel);
                documentiMap.Staff = mapper.Map<StaffModel>(responsible_cargo);

                listDocumentiModel.Add(documentiMap);
            }
            return listDocumentiModel;
        }

        async Task<DocumentiModel?> IDocumentiService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await documentiReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var cargo = await cargoReadRepository.GetByIdAsync(item.CargoId, cancellationToken);
            var vessel = await vesselReadRepository.GetByIdAsync(item.VesselId, cancellationToken);
            var staff = await staffReadRepository.GetByIdAsync(item.StaffId, cancellationToken);
            var doc = mapper.Map<DocumentiModel>(item);
            doc.Cargo = cargo != null
                ? mapper.Map<CargoModel>(cargo)
                : null;
            doc.Vessel = vessel != null
                ? mapper.Map<VesselModel>(vessel)
                : null;
            doc.Staff = staff != null
                ? mapper.Map<StaffModel>(staff)
                : null;
            return doc;
        }

        async Task<DocumentiModel> IDocumentiService.AddAsync(DocumentiRequestModel documenti, CancellationToken cancellationToken)
        {
            var item = new Documenti
            {
                Id = Guid.NewGuid(),
                Number = documenti.Number,
                IssaedAt = documenti.IssaedAt,
                CargoId = documenti.CargoId,
                VesselId = documenti.VesselId,
                StaffId = documenti.StaffId
            };

            documentiWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DocumentiModel>(item);
        }

        async Task<DocumentiModel> IDocumentiService.UpdateAsync(DocumentiRequestModel source, CancellationToken cancellationToken)
        {
            var targetDoc = await documentiReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetDoc == null)
            {
                throw new PortEntityNotFoundException<Documenti>(source.Id);
            }
            targetDoc.Number = source.Number;

            var cargo = await cargoReadRepository.GetByIdAsync(source.CargoId, cancellationToken);
            targetDoc.CargoId = cargo.Id;

            var vessel = await vesselReadRepository.GetByIdAsync(source.VesselId, cancellationToken);
            targetDoc.VesselId = vessel.Id;

            var staff = await staffReadRepository.GetByIdAsync(source.StaffId, cancellationToken);
            targetDoc.StaffId = staff.Id;

            documentiWriteRepository.Update(targetDoc);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<DocumentiModel>(targetDoc);
        }


        async Task IDocumentiService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetDoc = await documentiReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetDoc == null)
            {
                throw new PortEntityNotFoundException<Documenti>(id);
            }
            if (targetDoc.DeletedAt.HasValue)
            {
                throw new PortInvalidOperationException($"Документ с идентификатором {id} уже удален");
            }

            documentiWriteRepository.Delete(targetDoc);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
