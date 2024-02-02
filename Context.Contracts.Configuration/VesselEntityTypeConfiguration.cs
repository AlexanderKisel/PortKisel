﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortKisel.Context.Contracts.Models;

namespace PortKisel.Context.Configuration
{
    public class VesselEntityTypeConfiguration : IEntityTypeConfiguration<Vessel>
    {
        public void Configure(EntityTypeBuilder<Vessel> builder)
        {
            builder.ToTable("Vessel");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.CompanyPerId).IsRequired();
            builder.Property(x => x.LoadCapacity).IsRequired();

            builder
                .HasMany(x => x.Documenti)
                .WithOne(x => x.Vessel)
                .HasForeignKey(x => x.VesselId);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(Vessel.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Vessel)}_{nameof(Vessel.Name)}");

            builder.HasIndex(x => x.LoadCapacity)
                .HasFilter($"{nameof(Vessel.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Vessel)}_{nameof(Vessel.LoadCapacity)}");
        }
    }
}
