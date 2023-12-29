using Microsoft.EntityFrameworkCore;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Common.Entity.Repositories;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;


namespace PortKisel.Repositories.Implementations
{
    public class CompanyZakazchikReadRepository : ICompanyZakazchikReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public CompanyZakazchikReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<List<CompanyZakazchik>> ICompanyZakazchikReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<CompanyZakazchik>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Description)
            .ToListAsync(cancellationToken);

        Task<CompanyZakazchik?> ICompanyZakazchikReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<CompanyZakazchik>()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, CompanyZakazchik>> ICompanyZakazchikReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<CompanyZakazchik>()
            .NotDeletedAt()
            .ByIds(ids)
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Description)
            .ToDictionaryAsync(key => key.Id, cancellationToken);
    }
}
