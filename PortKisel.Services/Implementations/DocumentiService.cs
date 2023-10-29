using AutoMapper;
using PortKisel.Services.Anchors;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.Models.Enums;

namespace PortKisel.Services.Implementations
{
    public class DocumentiService : IDocumentiService, IServiceAnchor
    {
        private readonly IDocumentiReadRepository documentiReadRepository;
        private readonly 

        public DocumentiService(IDocumentiReadRepository documentiReadRepository)
        {
            this.documentiReadRepository = documentiReadRepository;
        }
        async Task<IEnumerable<DocumentiModel>> IDocumentiService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await documentiReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new DocumentiModel
            {
                Id = x.Id,
                NumberDoc = x.NumberDoc,
                IssaedAt = x.IssaedAt,
                CargoName = x.CargoId,
                VesselName = x.VesselId,
                CompanyPerName = x.CompanyPerId,
                CompanyZakazchikName = x.CompanyZakazchikId,
                Posts = (PostModels)x.Posts,
            });
        }

        async Task<DocumentiModel?> IDocumentiService.GetByAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await documentiReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return new DocumentiModel
            {
                Id = item.Id,
                NumberDoc = item.NumberDoc,
                IssaedAt = item.IssaedAt,
                CargoId = item.CargoId,
                VesselId = item.VesselId,
                CompanyPerId = item.CompanyPerId,
                CompanyZakazchikId = item.CompanyZakazchikId,
                Posts = (PostModels)item.Posts,
            };
        }
    }
}
