using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    /// <inheritdoc cref="IDocumentiWriteRepository"/>
    public class DocumentiWriteRepository : BaseWriteRepository<Documenti>,
        IDocumentiWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyPerWriteRepository"/>
        /// </summary>
        public DocumentiWriteRepository(IDbWriterContext writerContext)
            : base(writerContext) { }
    }
}