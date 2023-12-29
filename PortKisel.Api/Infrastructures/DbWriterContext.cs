using PortKisel.Common;
using PortKisel.Common.Entity.InterfaceDB;

namespace PortKisel.Api.Infrastructures
{
    /// <inheritdoc cref="IDbWriterContext"/>
    public class DbWriterContext : IDbWriterContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DbWriterContext"/>
        /// </summary>
        /// <remarks>В реальном проекте надо добавлять IIdentity для доступа к
        /// информации об авторизации</remarks>
        public DbWriterContext(
            IDbWriter writer,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            DateTimeProvider = dateTimeProvider;
        }

        public IDbWriter Writer { get; }

        public IUnitOfWork UnitOfWork { get; }

        public IDateTimeProvider DateTimeProvider { get; }

        public string UserName { get; } = "PortKisel.Api";
    }
}
