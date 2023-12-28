using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Common.Entity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PortKisel.Repositories.Implementations
{
    public class StaffReadRepository : IStaffReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public StaffReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<List<Staff>> IStaffReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Staff>()
            .NotDeletedAt()
            .OrderBy(x => x.FIO)
            .ThenBy(x => x.Post)
            .ToListAsync(cancellationToken);

        Task<Staff?> IStaffReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Staff>()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Staff>> IStaffReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Staff>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.FIO)
                .ThenBy(x => x.Post)
                .ToDictionaryAsync(key => key.Id, cancellationToken);
    }
}