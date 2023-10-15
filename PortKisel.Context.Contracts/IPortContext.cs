using PortKisel.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortKisel.Context.Contracts
{
    public interface IPortContext
    {
        IEnumerable<Cargo> Cargos { get; }

        IEnumerable<CompanyPer> CompanyPers { get; }

        IEnumerable<CompanyZakazchik> CompanyZakazchiks { get; }
        IEnumerable<Documenti> Documents { get; }
        IEnumerable<Staff> Staff { get; }
        IEnumerable<Vessel> Vessels { get; }
    }
}
