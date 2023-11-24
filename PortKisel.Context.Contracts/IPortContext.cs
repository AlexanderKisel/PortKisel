using PortKisel.Context.Contracts.Models;

namespace PortKisel.Context.Contracts
{
    public interface IPortContext
    {
        IEnumerable<Cargo> Cargos { get; }

        IEnumerable<CompanyPer> CompanyPers { get; }

        IEnumerable<CompanyZakazchik> CompanyZakazchiks { get; }
        IEnumerable<Documenti> Documents { get; }
        IEnumerable<Staff> Staffs { get; }
        IEnumerable<Vessel> Vessels { get; }
    }
}
