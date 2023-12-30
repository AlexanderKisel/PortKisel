using Microsoft.EntityFrameworkCore;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Common.Entity.Repositories;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class DocumentiReadRepository : IDocumentiReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public DocumentiReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<List<Documenti>> IDocumentiReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Documenti>()
            .NotDeletedAt()
            .OrderBy(x => x.Number)
            .ThenBy(x => x.IssaedAt)
            .ToListAsync(cancellationToken);

        Task<Documenti?> IDocumentiReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Documenti>()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Documenti>> IDocumentiReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Documenti>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Number)
                .ThenBy(x => x.IssaedAt)
                .ToDictionaryAsync(key => key.Id, cancellationToken);

        Task<bool> IDocumentiReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Documenti>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
