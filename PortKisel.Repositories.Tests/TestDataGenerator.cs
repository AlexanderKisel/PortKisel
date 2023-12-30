using PortKisel.Context.Contracts.Enums;
using PortKisel.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortKisel.Repositories.Tests
{
    static internal class TestDataGenerator
    {
        static internal Cargo Cargo(Action<Cargo>? action = null)
        {
            var item = new Cargo
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                Weight = "10000",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid()}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"CreatedBy{Guid.NewGuid()}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal CompanyPer CompanyPer(Action<CompanyPer>? action = null)
        {
            var item = new CompanyPer
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid()}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"CreatedBy{Guid.NewGuid()}",
            };

            action?.Invoke(item);
            return item;
        }


        static internal CompanyZakazchik CompanyZakazchik(Action<CompanyZakazchik>? action = null)
        {
            var item = new CompanyZakazchik
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid()}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"CreatedBy{Guid.NewGuid()}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Documenti Documenti(Action<Documenti>? action = null)
        {
            var item = new Documenti
            {
                Id = Guid.NewGuid(),
                Number = $"Number{Guid.NewGuid()}",
                IssaedAt = DateTime.UtcNow,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid()}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"CreatedBy{Guid.NewGuid()}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Staff Staff(Action<Staff>? action = null)
        {
            var item = new Staff
            {
                Id = Guid.NewGuid(),
                FIO = $"Name{Guid.NewGuid()}",
                Post = Posts.Responsible_cargo,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid()}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"CreatedBy{Guid.NewGuid()}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Vessel Vessel(Action<Vessel>? action = null)
        {
            var item = new Vessel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                LoadCapacity = "12000",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid()}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"CreatedBy{Guid.NewGuid()}",
            };

            action?.Invoke(item);
            return item;
        }
    }
}
