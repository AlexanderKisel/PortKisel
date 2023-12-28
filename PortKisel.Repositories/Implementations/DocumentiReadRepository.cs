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
    }
}
