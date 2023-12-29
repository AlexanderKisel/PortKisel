using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Repositories.Implementations
{
    /// <inheritdoc cref="ICargoWriteRepository"/>
    public class CargoWriteRepository : BaseWriteRepository<Cargo>,
        ICargoWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CargoWriteRepository"/>
        /// </summary>
        public CargoWriteRepository(IDbWriterContext writerContext)
            : base(writerContext) { }
    }
}
