using PortKisel.Context.Contracts;
using PortKisel.Context.Contracts.Models;

namespace PortKisel.Context
{
    public class PortContext: IPortContext
    {
        private readonly IList<Cargo> cargos;
        private readonly IList<CompanyPer> companypers;
        private readonly IList<CompanyZakazchik> companyzakazchiks;
        private readonly IList<Documenti> documents;
        private readonly IList<Staff> staffs;
        private readonly IList<Vessel> vessels;

        public PortContext()
        {
            cargos = new List<Cargo>();
            companypers = new List<CompanyPer>();
            companyzakazchiks = new List<CompanyZakazchik>();
            documents = new List<Documenti>();
            staffs = new List<Staff>();
            vessels = new List<Vessel>();
        }

        IEnumerable<Cargo> IPortContext.Cargos => cargos;
        IEnumerable<CompanyPer> IPortContext.CompanyPers => companypers;
        IEnumerable<CompanyZakazchik> IPortContext.CompanyZakazchiks => companyzakazchiks;
        IEnumerable<Documenti> IPortContext.Documents => documents;
        IEnumerable<Staff> IPortContext.Staffs => staffs;
        IEnumerable<Vessel> IPortContext.Vessels => vessels;
    }
}