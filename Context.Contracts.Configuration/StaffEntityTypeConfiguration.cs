using PortKisel.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortKisel.Context.Configuration
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.FIO).IsRequired();
            builder.Property(x => x.Post).IsRequired();

            builder
                .HasMany(x => x.Documenti)
                .WithOne(x => x.Staff)
                .HasForeignKey(x => x.Responsible_cargoId);
        }
    }
}
