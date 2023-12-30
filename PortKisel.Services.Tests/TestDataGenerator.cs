using PortKisel.Context.Contracts.Enums;
using PortKisel.Context.Contracts.Models;
using PortKisel.Services.Contracts.Models.Enums;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Tests
{
    static public class TestDataGenerator
    {
        static public Cargo Cargo(Action<Cargo>? action = null)
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

        static public CompanyPer CompanyPer(Action<CompanyPer>? action = null)
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


        static public CompanyZakazchik CompanyZakazchik(Action<CompanyZakazchik>? action = null)
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

        static public Documenti Documenti(Action<Documenti>? action = null)
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

        static public Staff Staff(Action<Staff>? action = null)
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

        static public Vessel Vessel(Action<Vessel>? action = null)
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

        static public CargoRequestModel CargoRequestModel(Action<CargoRequestModel>? action = null)
        {
            var item = new CargoRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                Weight = "10000"
            };

            action?.Invoke(item);
            return item;
        }

        static public CompanyPerRequestModel CompanyPerRequestModel(Action<CompanyPerRequestModel>? action = null)
        {
            var item = new CompanyPerRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}"
            };

            action?.Invoke(item);
            return item;
        }


        static public CompanyZakazchikRequestModel CompanyZakazchikRequestModel(Action<CompanyZakazchikRequestModel>? action = null)
        {
            var item = new CompanyZakazchikRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}"
            };

            action?.Invoke(item);
            return item;
        }

        static public DocumentiRequestModel DocumentiRequestModel(Action<DocumentiRequestModel>? action = null)
        {
            var item = new DocumentiRequestModel
            {
                Id = Guid.NewGuid(),
                Number = $"Number{Guid.NewGuid()}",
                IssaedAt = DateTime.UtcNow
            };

            action?.Invoke(item);
            return item;
        }

        static public StaffRequestModel StaffRequestModel(Action<StaffRequestModel>? action = null)
        {
            var item = new StaffRequestModel
            {
                FIO = $"Name{Guid.NewGuid()}",
                Post = PostModels.Responsible_cargo
            };

            action?.Invoke(item);
            return item;
        }

        static public VesselRequestModel VesselRequestModel(Action<VesselRequestModel>? action = null)
        {
            var item = new VesselRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid()}",
                Description = $"Description{Guid.NewGuid()}",
                LoadCapacity = "12000"
            };

            action?.Invoke(item);
            return item;
        }
    }
}
