using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class DocumentiReadRepository : IDocumentiReadRepository, IRepositoryAnchor
    {
        private readonly IPortContext context;

        public DocumentiReadRepository(IPortContext context)
        {
            this.context = context;
        }

        Task<List<Documenti>> IDocumentiReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Documents.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Number)
                .ToList());

        Task<Documenti?> IDocumentiReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Documents.FirstOrDefault(x => x.Id == id));
    }
}
