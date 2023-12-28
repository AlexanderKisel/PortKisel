using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    /// <inheritdoc cref="ICompanyPerWriteRepository"/>
    public class CompanyPerWriteRepository : BaseWriteRepository<CompanyPer>,
        ICompanyPerWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyPerWriteRepository"/>
        /// </summary>
        public CompanyPerWriteRepository(IDbWriterContext writerContext)
            : base(writerContext) { }
    }
}