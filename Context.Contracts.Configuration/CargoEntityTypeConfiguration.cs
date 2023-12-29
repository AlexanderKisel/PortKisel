using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortKisel.Context.Contracts.Models;

namespace PortKisel.Context.Configuration
{
    public class CargoEntityTypeConfiguration : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.ToTable("Cargo");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Weight).IsRequired();
            builder.Property(x => x.CompanyZakazchikId).IsRequired();

            builder
                .HasMany(x => x.Documenti)
                .WithOne(x => x.Cargo)
                .HasForeignKey(x => x.CargoId);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(Cargo.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Cargo)}_{nameof(Cargo.Name)}");

            builder.HasIndex(x => x.Weight)
                .IsUnique()
                .HasFilter($"{nameof(Cargo.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Cargo)}_{nameof(Cargo.Weight)}");
        }
    }
}
