using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortKisel.Context.Contracts.Models;

namespace PortKisel.Context.Configuration
{
    public class CompanyZakazchikEntityTypeConfiguration : IEntityTypeConfiguration<CompanyZakazchik>
    {
        public void Configure(EntityTypeBuilder<CompanyZakazchik> builder)
        {
            builder.ToTable("CompanyZakazchik");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder
                .HasMany(x => x.Cargo)
                .WithOne(x => x.CompanyZakazchik)
                .HasForeignKey(x => x.CompanyZakazchikId);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(CompanyPer.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(CompanyPer)}_{nameof(CompanyPer.Name)}");
        }
    }
}
