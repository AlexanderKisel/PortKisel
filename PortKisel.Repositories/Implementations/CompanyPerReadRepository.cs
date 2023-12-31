﻿using Microsoft.EntityFrameworkCore;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Common.Entity.Repositories;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    public class CompanyPerReadRepository : ICompanyPerReadRepository, IRepositoryAnchor
    {
        private readonly IDbRead reader;

        public CompanyPerReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }
        Task<bool> ICompanyPerReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
                => reader.Read<CompanyPer>().NotDeletedAt().AnyAsync(x => x.Id == id, cancellationToken);
        Task<List<CompanyPer>> ICompanyPerReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<CompanyPer>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Description)
            .ToListAsync(cancellationToken);

        Task<CompanyPer?> ICompanyPerReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<CompanyPer>()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, CompanyPer>> ICompanyPerReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<CompanyPer>()
            .NotDeletedAt()
            .ByIds(ids)
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Description)
            .ToDictionaryAsync(key => key.Id, cancellationToken);
        Task<bool> ICompanyPerReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<CompanyPer>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
