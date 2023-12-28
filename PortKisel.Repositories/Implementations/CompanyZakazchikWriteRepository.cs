using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    /// <inheritdoc cref="ICompanyZakazchikWriteRepository"/>
    public class CompanyZakazchikWriteRepository : BaseWriteRepository<CompanyZakazchik>,
        ICompanyZakazchikWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyPerWriteRepository"/>
        /// </summary>
        public CompanyZakazchikWriteRepository(IDbWriterContext writerContext)
            : base(writerContext) { }
    }
}