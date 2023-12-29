using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortKisel.Context.Contracts.Models;

namespace PortKisel.Context.Configuration
{
    public class CompanyPerEntityTypeConfiguration : IEntityTypeConfiguration<CompanyPer>
    {
        public void Configure(EntityTypeBuilder<CompanyPer> builder)
        {
            builder.ToTable("CompanyPer");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder
                .HasMany(x => x.Vessels)
                .WithOne(x => x.CompanyPer)
                .HasForeignKey(x => x.CompanyPerId);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(CompanyPer.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(CompanyPer)}_{nameof(CompanyPer.Name)}");
        }
    }
}
