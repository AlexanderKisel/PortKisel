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
    public class CompanyZakazchikService : ICompanyZakazchikService, IServiceAnchor
    {
        private readonly ICompanyZakazchikReadRepository companyZakazchikReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompanyZakazchikWriteRepository companyZakazchikWriteRepository;

        public CompanyZakazchikService(ICompanyZakazchikReadRepository companyZakazchikReadRepository, IMapper mapper, IUnitOfWork unitOfWork, ICompanyZakazchikWriteRepository companyZakazchikWriteRepository)
        {
            this.companyZakazchikReadRepository = companyZakazchikReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.companyZakazchikWriteRepository = companyZakazchikWriteRepository;
        }

        async Task<IEnumerable<CompanyZakazchikModel>> ICompanyZakazchikService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await companyZakazchikReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<CompanyZakazchikModel>>(result);
        }

        async Task<CompanyZakazchikModel?> ICompanyZakazchikService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await companyZakazchikReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            return mapper.Map<CompanyZakazchikModel>(item);
        }
        async Task<CompanyZakazchikModel> ICompanyZakazchikService.AddAsync(CompanyZakazchikRequestModel companyPer, CancellationToken cancellationToken)
        {
            var item = new CompanyZakazchik
            {
                Id = Guid.NewGuid(),
                Name = companyPer.Name,
                Description = companyPer.Description,
            };

            companyZakazchikWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CompanyZakazchikModel>(item);
        }

        async Task<CompanyZakazchikModel> ICompanyZakazchikService.UpdateAsync(CompanyZakazchikRequestModel source, CancellationToken cancellationToken)
        {
            var targetCompanyPer = await companyZakazchikReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetCompanyPer == null)
            {
                throw new PortEntityNotFoundException<CompanyPer>(source.Id);
            }

            targetCompanyPer.Name = source.Name;
            targetCompanyPer.Description = source.Description;

            companyZakazchikWriteRepository.Update(targetCompanyPer);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CompanyZakazchikModel>(targetCompanyPer);
        }
        async Task ICompanyZakazchikService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetSupplier = await companyZakazchikReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetSupplier == null)
            {
                throw new PortEntityNotFoundException<CompanyPer>(id);
            }
            if (targetSupplier.DeletedAt.HasValue)
            {
                throw new PortInvalidOperationException($"Документ с идентификатором {id} уже удален");
            }

            companyZakazchikWriteRepository.Delete(targetSupplier);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
