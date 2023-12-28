using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    /// <inheritdoc cref="IVesselWriteRepository"/>
    public class VesselWriteRepository : BaseWriteRepository<Vessel>,
        IVesselWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyPerWriteRepository"/>
        /// </summary>
        public VesselWriteRepository(IDbWriterContext writerContext)
            : base(writerContext) { }
    }
}