using PortKisel.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortKisel.Context.Configuration
{
    public class DocumentiEntityTypeConfiguration : IEntityTypeConfiguration<Documenti>
    {
        public void Configure(EntityTypeBuilder<Documenti> builder)
        {
            builder.ToTable("CompanyZakazchik");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.IssaedAt).IsRequired();
            builder.Property(x => x.CargoId).IsRequired();
            builder.Property(x => x.VesselId).IsRequired();
            builder.Property(x => x.Responsible_cargoId).IsRequired();
        }
    }
}
