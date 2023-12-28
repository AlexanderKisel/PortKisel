using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    /// <inheritdoc cref="IStaffWriteRepository"/>
    public class StaffWriteRepository : BaseWriteRepository<Staff>,
        IStaffWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyPerWriteRepository"/>
        /// </summary>
        public StaffWriteRepository(IDbWriterContext writerContext)
            : base(writerContext) { }
    }
}