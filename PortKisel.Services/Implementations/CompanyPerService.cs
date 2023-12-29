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
    public class CompanyPerService : ICompanyPerService, IServiceAnchor
    {
        private readonly ICompanyPerReadRepository companyPerReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompanyPerWriteRepository companyPerWriteRepository;

        public CompanyPerService(ICompanyPerReadRepository companyPerReadRepository, IMapper mapper, IUnitOfWork unitOfWork, ICompanyPerWriteRepository companyPerWriteRepository)
        {
            this.companyPerReadRepository = companyPerReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.companyPerWriteRepository = companyPerWriteRepository;
        }



        async Task<IEnumerable<CompanyPerModel>> ICompanyPerService.GetAllAsync(System.Threading.CancellationToken cancellationToken)
        {
            var result = await companyPerReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<CompanyPerModel>>(result);
        }

        async Task<CompanyPerModel?> ICompanyPerService.GetByIdAsync(System.Guid id, System.Threading.CancellationToken cancellationToken)
        {
            var item = await companyPerReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            return mapper.Map<CompanyPerModel>(item);
        }
        async Task<CompanyPerModel> ICompanyPerService.AddAsync(CompanyPerRequestModel companyPer, CancellationToken cancellationToken)
        {
            var item = new CompanyPer
            {
                Id = Guid.NewGuid(),
                Name = companyPer.Name,
                Description = companyPer.Description,
            };

            companyPerWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CompanyPerModel>(item);
        }

        async Task<CompanyPerModel> ICompanyPerService.UpdateAsync(CompanyPerRequestModel source, CancellationToken cancellationToken)
        {
            var targetCompanyPer = await companyPerReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetCompanyPer == null)
            {
                throw new PortEntityNotFoundException<CompanyPer>(source.Id);
            }

            targetCompanyPer.Name = source.Name;
            targetCompanyPer.Description = source.Description;

            companyPerWriteRepository.Update(targetCompanyPer);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CompanyPerModel>(targetCompanyPer);
        }
        async Task ICompanyPerService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetSupplier = await companyPerReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetSupplier == null)
            {
                throw new PortEntityNotFoundException<CompanyPer>(id);
            }
            if (targetSupplier.DeletedAt.HasValue)
            {
                throw new PortInvalidOperationException($"Документ с идентификатором {id} уже удален");
            }

            companyPerWriteRepository.Delete(targetSupplier);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
