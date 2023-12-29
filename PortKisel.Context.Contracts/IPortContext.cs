using Microsoft.EntityFrameworkCore;
using PortKisel.Context.Contracts.Models;

namespace PortKisel.Context.Contracts
{
    /// <summary>
    /// Работа с сущностями
    /// </summary>
    public interface IPortContext
    {
        /// <summary>
        /// Список <inheritdoc cref="Cargo"/>
        /// </summary>
        DbSet<Cargo> Cargos { get; }

        /// <summary>
        /// Список <inheritdoc cref="Vessel"/>
        /// </summary>
        DbSet<Vessel> Vessels { get; }

        /// <summary>
        /// Список <inheritdoc cref="CompanyPer"/>
        /// </summary>
        DbSet<CompanyPer> CompanyPers { get; }

        /// <summary>
        /// Список <inheritdoc cref="CompanyZakazchik"/>
        /// </summary>
        DbSet<CompanyZakazchik> CompanyZakazchiks { get; }

        /// <summary>
        /// Список <inheritdoc cref="Documenti"/>
        /// </summary>
        DbSet<Documenti> Documentis { get; }

        /// <summary>
        /// Список <inheritdoc cref="Staff"/>
        /// </summary>
        DbSet<Staff> Staffs { get; }
    }
}
