using Microsoft.EntityFrameworkCore;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Configuration;
using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;

namespace PortKisel.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project PortKisel.Context\PortKisel.Context.csproj
    /// 4) dotnet ef database update --project PortKisel.Context\PortKisel.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --PortKisel.Context\PortKisel.Context.csproj
    /// </remarks>
    public class PortContext : DbContext,
        IPortContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork
    {
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<CompanyPer> CompanyPers { get; set; }
        public DbSet<CompanyZakazchik> CompanyZakazchiks { get; set; }
        public DbSet<Documenti> Documentis { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public PortContext(DbContextOptions<PortContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IContextConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntities>(TEntities entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Deleted;


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            SkipTracker();
            return count;
        }

        public void SkipTracker()
        {
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}